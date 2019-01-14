using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SalleScript : MonoBehaviour {

	public Material normalMaterial;
	public Material onCardDrag;
	MeshRenderer mr;
	public int id;
	public SpriteRenderer sr;


	void Awake () 
	{
		if (mr == null)
		{
			mr = GetComponent<MeshRenderer>();
			mr.material = normalMaterial;
		}
	}
	
	
	void Update () {
		
	}

	public void OnDragCardOnMe()
	{
		mr.material = onCardDrag;
	}

	public void OnExitCardOnMe()
	{
		mr.material = normalMaterial;
	}
}
