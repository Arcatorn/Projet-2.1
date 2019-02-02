﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class CartesIndicators : MonoBehaviour, IEventSystemHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Animator animator;
    CartesManager cartesManager;
	public Image myImage;
	Color persoBleu = new Color32(124,124,255,185);
	Color persoRouge = new Color32(255, 163,124,185);
	GameMaster gameMaster;
	public Image imageNumero;


    private void Awake()
    {
        cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
		myImage = GetComponent<Image>();
		gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
		imageNumero = transform.GetChild(0).gameObject.GetComponent<Image>();
    }


    public void OnPointerClick(PointerEventData pointerEvent)
    {
        if (!animator.GetBool("Interaction"))
        {
            pointerEvent = null;
        }
        else
        {
            animator.SetBool("VaLaChercher", true);
            animator.SetBool("Interaction", false);
			myImage.color = GiveMeColorPersoActif();
            int _wantedCardID = int.Parse(myImage.sprite.name.Substring(5, 1));
            gameMaster.pc[gameMaster.playerActif].destination = cartesManager.cartesPhysiques[_wantedCardID].transform.position;
            gameMaster.BeforeCancelOrder();
            gameMaster.persoScripts[gameMaster.playerActif].CancelOrder();
            gameMaster.persoScripts[gameMaster.playerActif].OrderGoGetACard(_wantedCardID);
			cartesManager.SortCartesIndicators();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!animator.GetBool("Interaction"))
        {
            eventData = null;
        }
        else
        {
            animator.SetBool("CursorOn", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!animator.GetBool("Interaction"))
        {
            eventData = null;
        }
        else
        {
            animator.SetBool("CursorOn", false);
        }
    }

	public void ImRassable()
	{
		animator.SetBool("Interaction", true);
		animator.SetBool("CursorOn", false);
		animator.SetBool("Full", false);
		animator.SetBool("VaLaChercher", false);
		animator.SetBool("Special", false);
		myImage.color = new Color32(255, 255, 255, 185);
	}

	public void ImAutrePerso()
	{
		animator.SetBool("Interaction", false);
		animator.SetBool("Full", false);
		animator.SetBool("CursorOn", false);
		animator.SetBool("VaLaChercher", false);
		animator.SetBool("Special", false);
		myImage.color = GiveMeColorAutrePerso();
	}

	public void ImAutrePersoVaLaChercher()
	{
		animator.SetBool("Interaction", false);
		animator.SetBool("Full", false);
		animator.SetBool("CursorOn", false);
		animator.SetBool("VaLaChercher", true);
		animator.SetBool("Special", false);
		myImage.color = GiveMeColorAutrePerso();
	}

	public void ImAutrePersoVaLaJouer()
	{
		animator.SetBool("Interaction", false);
		animator.SetBool("Full", false);
		animator.SetBool("CursorOn", false);
		animator.SetBool("VaLaChercher", false);
		animator.SetBool("Special", true);
		myImage.color = GiveMeColorAutrePerso();
	}

	public void ImUsedByConsole()
	{
		animator.SetBool("Interaction", false);
		animator.SetBool("Full", false);
		animator.SetBool("CursorOn", false);
		animator.SetBool("VaLaChercher", false);
		animator.SetBool("Special", false);
		myImage.color = new Color32(80, 255, 80, 185);
	}

	public void ImVaLaChercher()
	{
        animator.SetBool("Interaction", false);
		animator.SetBool("Full", false);
		animator.SetBool("VaLaChercher", true);
		animator.SetBool("Special", false);
		myImage.color = GiveMeColorPersoActif();
	}

	public void ImDisabled()
	{
		animator.SetBool("Interaction", false);
		animator.SetBool("Full", false);
		animator.SetBool("CursorOn", false);
		animator.SetBool("Special", false);
		myImage.sprite = Resources.Load <Sprite> ("Sprites/Cartes/CarteBlank2");
		imageNumero.sprite = Resources.Load <Sprite> ("Sprites/Cartes/CarteBlank2");
	}

	public void ImRassableFull()
	{
		animator.SetBool("Interaction", false);
		animator.SetBool("CursorOn", false);
		animator.SetBool("Full", true);
		animator.SetBool("Special", false);
		myImage.color = new Color32(255, 255, 255, 20);
	}

	private Color GiveMeColorAutrePerso()
	{
		Color toReturn = persoBleu;
		if (gameMaster.playerActif == 1)
		{
			toReturn = persoRouge;
		}
		return toReturn;
	}

	private Color GiveMeColorPersoActif()
	{
		Color toReturn = persoBleu;
		if (gameMaster.playerActif == 0)
		{
			toReturn = persoRouge;
		}
		return toReturn;
	}

}
