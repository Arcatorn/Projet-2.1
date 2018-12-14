using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaster : MonoBehaviour {

	public GameObject caravaneJoueur;
	public GameObject caravaneEnnemi;

	public Vector3[] midPos;

	void Start () {
		
	}
	

	void Update () {
		
	}

	Vector3 SelectPosPlayer()
	{
		float x = Random.Range(-8, -1);
		float y = Random.Range(-4.5f, 4.5f);
		return new Vector3(x, y, 0);
	}

	Vector3 SelectPosEnnemi()
	{
		float x = Random.Range(1, 8);
		float y = Random.Range(-4.5f, 4.5f);
		return new Vector3(x, y, 0);
	}
}
