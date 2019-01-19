﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class CartesButtons : MonoBehaviour, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{

    [HideInInspector]
    public Animator anim;
    private CardSound cardSound;
    CartesManager cartesManager;
    public int id;
    public TextMeshProUGUI textMeshProComponent;
    private Image myImage;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
        cardSound = Camera.main.GetComponent<CardSound>();
        myImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (anim.GetBool("isBlank") || anim.GetBool("isPlayed"))
        {
            eventData.pointerDrag = null;
        }
        else
        {
            anim.SetTrigger("Pressed");
            GameMaster.isPlayingACard = true;
            cardSound.HoldCard();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (anim.GetBool("isBlank") || anim.GetBool("isPlayed"))
        {
            eventData.pointerDrag = null;
        }
        else
        {
            if (GameMaster.hittingAModule)
            {
                int i = int.Parse(myImage.sprite.name.Substring(5, 1));
                GameMaster.cardIDBeingPlayed = i;
                anim.SetTrigger("Normal");
            }
            else
            {
                anim.SetTrigger("Normal");
                cardSound.CancelHolding();
            }
            GameMaster.endPlayingCard = true;
            GameMaster.cursorIsOnCard = false;
            GameMaster.isPlayingACard = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (anim.GetBool("isBlank") || anim.GetBool("isPlayed"))
        {
            eventData.pointerDrag = null;
        }
        else
        {
            if (!GameMaster.isPlayingACard)
            {
                anim.SetBool("CursorOn", true);
                GameMaster.cursorIsOnCard = true;
                cardSound.HoverCard();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (anim.GetBool("isBlank") || anim.GetBool("isPlayed"))
        {
            eventData.pointerDrag = null;
        }
        else
        {
            anim.SetBool("CursorOn", false);
            GameMaster.cursorIsOnCard = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
		if (anim.GetBool("isBlank") || anim.GetBool("isPlayed"))
        {
            eventData.pointerDrag = null;
        }
    }

    private void Update() {
        /*if (anim.GetBool("isBlank"))
        {
            Debug.Log(myImage.color);
            cartesManager.ForceChangeColorBlank(myImage);
        }*/
    }
}
