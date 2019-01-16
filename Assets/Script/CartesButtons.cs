using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum ButtonStates
{
	disabled = 0,
	normal = 1,
	highlighted = 2,
	hold = 3,
	release = 4
}

public class CartesButtons : MonoBehaviour, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{

	[HideInInspector]
	public ButtonStates buttonState;
	Animator anim;
	private CardSound cardSound;
	CartesManager cartesManager;
	public Canvas myCanvas;
	public GameObject ps;
	public int id;
	public TextMeshProUGUI textMeshProComponent;
	
	
	private void Awake() {
		anim = GetComponent<Animator>();
		buttonState = ButtonStates.normal;
		cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
		cardSound = Camera.main.GetComponent<CardSound>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//if (buttonState == ButtonStates.highlighted)
		//{
			anim.SetTrigger("Pressed");
			buttonState = ButtonStates.hold;
			GameMaster.isPlayingACard = true;
			cardSound.HoldCard();
		//}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//if (buttonState == ButtonStates.hold)
		//{
		//	buttonState = ButtonStates.release;
		//}

		if (GameMaster.hittingAModule)
            {
                GameMaster.cardIDBeingPlayed = id;
            	//buttonState = ButtonStates.disabled;
				anim.SetTrigger("Normal");
            }
			else {
				//buttonState = ButtonStates.normal;
				anim.SetTrigger("Normal");
			cardSound.CancelHolding();
			}
            GameMaster.endPlayingCard = true;

		GameMaster.cursorIsOnCard = false;
		GameMaster.isPlayingACard = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//if (buttonState == ButtonStates.normal)
		//{
			anim.SetTrigger("Highlighted");
			buttonState = ButtonStates.highlighted;
			GameMaster.cursorIsOnCard = true;
		//}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//if (buttonState == ButtonStates.highlighted)
		//{
			anim.SetTrigger("Normal");
			buttonState = ButtonStates.normal;
			GameMaster.cursorIsOnCard = false;
		//}
	}

	public void OnDrag (PointerEventData eventData)
	{
		
	}

    private void Update()
    {
       /* if (buttonState == ButtonStates.release)
        {
            if (GameMaster.hittingAModule)
            {
                GameMaster.cardIDBeingPlayed = id;
            	buttonState = ButtonStates.disabled;
				anim.SetTrigger("Normal");
            }
			else {
				buttonState = ButtonStates.normal;
				anim.SetTrigger("Normal");
			}
            GameMaster.endPlayingCard = true;
        }*/
    }
}
