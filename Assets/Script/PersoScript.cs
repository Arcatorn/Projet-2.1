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
	public bool vaSurUneConsole = false;
	public int carteID = -1;
	public bool isConsoling = false;
	public ConsoleScript myConsole = null;
	public Animator PlayerAnim;
	CartesManager cartesManager;
	private CardSound cardSound;
	public GameObject WantedConsole;
	public int WantedCarteId;
	[SerializeField] private GameObject specialAction;

	public GameObject lumierePerso;
	
	void Start ()
	{
		lumierePerso = transform.GetChild(0).gameObject;
		nma = GetComponent<NavMeshAgent>();
		cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
		cardSound = Camera.main.GetComponent<CardSound>();
	}	
	
	void Update () 
	{
		if (vaJouerUneCarte)
		{
			if (!PlayerAnim.GetBool("GoRun"))
			{
				PlayerAnim.SetBool("GoRun", true);
			}
			JouerUneCarte(carteID);
		}
		else if (vaRamasserUneCarte)
		{
			if (!PlayerAnim.GetBool("GoRun"))
			{
				PlayerAnim.SetBool("GoRun", true);
			}
			RamasserUneCarte();
		}
		else if (vaSurUneConsole)
		{
			if (!PlayerAnim.GetBool("GoRun"))
			{
				PlayerAnim.SetBool("GoRun", true);
			}
			AllerSurUneConsole();
		}
		if (WantedConsole != null && WantedConsole.GetComponent<ConsoleScript>().persoOnMeID != monID )
		{
			CancelOrder();
		}
		DirectionFacing();	
	}

	public void JouerUneCarte(int cardID)
	{
		var remainingDistance = Vector3.Distance(transform.position, nma.destination);
		if (remainingDistance < 1.2f)
		{
			PlayerAnim.SetBool("GoRun",false);
			PlayerAnim.SetBool("PlayCard",true);
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
			cardSound.CardPickUp();
			PlayerAnim.SetBool("GoRun", false);
			PlayerAnim.SetTrigger("Grabbing");
		}
	}

	public void AllerSurUneConsole()
	{
		var remainingDistance = Vector3.Distance(transform.position, nma.destination);
		if (remainingDistance < 1.2f)
		{
			Vector3 relativePos = myConsole.gameObject.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
			myConsole.persoOnMe = true;
			myConsole.persoOnMeID = monID;
			PlayerAnim.SetBool("GoRun", false);
			PlayerAnim.SetBool("OnConsole", true);
			vaSurUneConsole = false;
			//nma.isStopped = true;
		}
	}

	public void CancelOrder()
	{
		PlayerAnim.SetBool("OnConsole", false);
		PlayerAnim.SetBool("GoRun", false);
		vaJouerUneCarte = false;
		vaRamasserUneCarte = false;
		vaSurUneConsole = false;
		WantedCarteId = -1;
		myConsole = null;
	}


    public void DirectionFacing()
    {
        if (nma.velocity.magnitude > 0.5f)
        {
            Vector3 relativePos = nma.velocity.normalized;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        }
    }

    public void OrderGoToConsole(ConsoleScript _myConsole)
    {
        if (myConsole != null)
        {
            myConsole.persoOnMe = false;
            myConsole.persoOnMeID = -1;
        }
        myConsole = _myConsole;
        PlayerAnim.SetBool("GoRun", true);
        vaSurUneConsole = true;
    }

    public void OrderGoPlayACard(int _cardId, ConsoleScript _myConsole)
    {
        carteID = _cardId;
        if (myConsole != null)
        {
            myConsole.persoOnMe = false;
            myConsole.persoOnMeID = -1;
        }
        myConsole = _myConsole;
        PlayerAnim.SetBool("GoRun", true);
    }

	public void OrderGoGetACard(int _wantedCardId)
	{
		WantedCarteId = _wantedCardId;
		PlayerAnim.SetBool("GoRun", true);
		vaRamasserUneCarte = true;
	}


}
