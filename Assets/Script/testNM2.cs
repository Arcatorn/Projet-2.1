using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class testNM2 : MonoBehaviour {

	public NavMeshAgent nm;
	public Camera camExt;
	public Camera camInt;

	public static bool chooseNewDir = false;
	public Image seperationScreen;
	

	void Update () 
	{
		if (chooseNewDir) 
		{
			MoveCaravane ();
		}
	}

	void MoveCaravane()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(camExt.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
			{
				nm.destination = hit.point;
				EndChosseDirection ();
			}
		}
	}

	public void DirectionController()
	{
		chooseNewDir = true;
		camExt.rect = new Rect (0, 0, 1, 1);
		camInt.rect = new Rect (1, 0, 0, 0);
		seperationScreen.enabled = false;
	}

	public void EndChosseDirection()
	{
		StartCoroutine ("cantact", 0.1f);
		camExt.rect = new Rect (0.5f, 0, 0.5f, 1);
		camInt.rect = new Rect (0, 0, 0.5f, 1);
		seperationScreen.enabled = true;
	}

	IEnumerator cantact(float delay)
	{
		yield return new WaitForSeconds(delay);
		chooseNewDir = false;
	}



}
