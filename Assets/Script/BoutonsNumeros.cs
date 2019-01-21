using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoutonsNumeros : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}

	
	public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }

	public void OnPointerClick(PointerEventData pointerEvent)
	{

	}
}
