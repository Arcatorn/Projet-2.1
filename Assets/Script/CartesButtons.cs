using System.Collections;
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
    public Canvas myCanvas;
    public GameObject ps;
    public int id;
    public TextMeshProUGUI textMeshProComponent;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
        cardSound = Camera.main.GetComponent<CardSound>();
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
                GameMaster.cardIDBeingPlayed = id;
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
}
