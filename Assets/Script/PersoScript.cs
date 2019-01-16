using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PersoScript : MonoBehaviour 
{

	NavMeshAgent nma;
	public int monID;
	public bool vaJouerUneCarte = false;
	public bool vaRamasserUneCarte = false;
	public int carteID = -1;
	
	CartesManager cartesManager;
	public ConsoleScript WantedConsoleScript;
	public int WantedCarteId;
	[SerializeField] private GameObject specialAction;
	
	void Start () 
	{
		nma = GetComponent<NavMeshAgent>();
		cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
	}	
	
	void Update () 
	{
		if (vaJouerUneCarte)
		{
			JouerUneCarte(carteID);
		}
		else if (vaRamasserUneCarte)
		{
			RamasserUneCarte();
		}	
	}

	public void JouerUneCarte(int cardID)
	{
		var remainingDistance = Vector3.Distance(transform.position, nma.destination);
		if (remainingDistance < 1.2f)
		{
			cartesManager.PlayACardOnModule(carteID);
			carteID = -1;
			vaJouerUneCarte = false;
		}
	}

	public void RamasserUneCarte()
	{
		var remainingDistance = Vector3.Distance(transform.position, nma.destination);
		if (remainingDistance < 2f)
		{
			cartesManager.AjouterUneCarteDansLaMain(monID, WantedCarteId);
			vaRamasserUneCarte = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.name == "Console")
		{
			var room = other.transform.parent;
			WantedConsoleScript = room.GetComponent<ConsoleScript>();
			if (!specialAction.activeInHierarchy)
			{
				specialAction.SetActive(true);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.parent.name == "Console")
		{
			WantedConsoleScript = null;
			if (specialAction.activeInHierarchy)
			{
				specialAction.SetActive(false);
			}
		}
	}
}
