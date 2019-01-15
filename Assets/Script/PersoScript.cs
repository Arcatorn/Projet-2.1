using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersoScript : MonoBehaviour {

	NavMeshAgent nma;
	public bool vaJouerUneCarte = false;
	public int carteID = -1;
	CartesManager cartesManager;
	
	void Start () {
		nma = GetComponent<NavMeshAgent>();
		cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
	}
	
	
	void Update () {
		if (vaJouerUneCarte)
		{
			JouerUneCarte(carteID);
		}
		
	}

	public void JouerUneCarte(int cardID)
	{
		var remainingDistance = Vector3.Distance(transform.position, nma.destination);
		if (remainingDistance < 1.2f)
		{
			Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), nma.destination, Quaternion.identity);
			cartesManager.PlayACardOnModule(carteID);
			carteID = -1;
			vaJouerUneCarte = false;
		}
	}
}
