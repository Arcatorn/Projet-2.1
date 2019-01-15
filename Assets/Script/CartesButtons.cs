using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

	public Canvas myCanvas;
	public GameObject ps;
	public int id;
	Image cd;
	float cdTimerMax = 5;
	float cdTimer = 0;
	
	private void Awake() {
		anim = GetComponent<Animator>();
		buttonState = ButtonStates.normal;
		cd = transform.GetChild(0).GetComponent<Image>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (buttonState == ButtonStates.highlighted)
		{
			anim.SetTrigger("Pressed");
			buttonState = ButtonStates.hold;
			GameMaster.isPlayingACard = true;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (buttonState == ButtonStates.hold)
		{
			buttonState = ButtonStates.release;
		}
		GameMaster.cursorIsOnCard = false;
		GameMaster.isPlayingACard = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (buttonState == ButtonStates.normal)
		{
			anim.SetTrigger("Highlighted");
			buttonState = ButtonStates.highlighted;
			GameMaster.cursorIsOnCard = true;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (buttonState == ButtonStates.highlighted)
		{
			anim.SetTrigger("Normal");
			buttonState = ButtonStates.normal;
			GameMaster.cursorIsOnCard = false;
		}
	}

	public void OnDrag (PointerEventData eventData)
	{
		
	}

    private void Update()
    {
        if (buttonState == ButtonStates.release)
        {
            if (GameMaster.hittingAModule)
            {
                GameMaster.cardIDBeingPlayed = id;
				cd.fillAmount = 1;
            	buttonState = ButtonStates.disabled;
				anim.SetTrigger("Disabled");
            }
			else {
				buttonState = ButtonStates.normal;
				anim.SetTrigger("Normal");
			}
            GameMaster.endPlayingCard = true;
        }
        else if (buttonState == ButtonStates.disabled)
        {
            cdTimer += Time.deltaTime;
            cd.fillAmount = 1 - (cdTimer / cdTimerMax);

            if (cdTimer >= cdTimerMax)
            {
                cdTimer = 0;
                buttonState = ButtonStates.normal;
				anim.SetTrigger("Normal");
            }
        }
    }

	public void MAFONCTIONTESTPOURDAMIEN()
	{
		Debug.Log("Au revoir");
	}
}
