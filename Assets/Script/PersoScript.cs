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
            JouerUneCarte(carteID);
            if (myConsole.persoOnMeID != monID && myConsole.persoOnMe)
            {
                cartesManager.CancelOrderAnimBlank(carteID, monID);
                CancelOrder();
                nma.SetDestination(transform.position);
            }
        }
		else if (vaRamasserUneCarte)
		{
			RamasserUneCarte();
		}
		else if (vaSurUneConsole)
		{
			AllerSurUneConsole();
			if (myConsole.persoOnMeID != monID && myConsole.persoOnMe)
			{
				CancelOrder();
                nma.SetDestination(transform.position);
			}
		}
		DirectionFacing();
	}

	public void JouerUneCarte(int cardID)
	{
		var remainingDistance = Vector3.Distance(transform.position, nma.destination);
		if (remainingDistance < 1.2f)
		{
			transform.position = nma.destination;
			myConsole.persoOnMe = true;
			myConsole.persoOnMeID = monID;
			PlayerAnim.SetBool("GoRun",false);
			PlayerAnim.SetTrigger("PlayCard");
			cartesManager.PlayACardOnModule(carteID, monID);
			carteID = -1;
			vaJouerUneCarte = false;
			isConsoling = true;
			//nma.SetDestination(transform.position);
			StartCoroutine("CoroutineForLookingAtConsole", 0.5f);
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
			transform.position = nma.destination;
			myConsole.persoOnMe = true;
			myConsole.persoOnMeID = monID;
			PlayerAnim.SetBool("GoRun", false);
			PlayerAnim.SetBool("OnConsole", true);
			vaSurUneConsole = false;
			isConsoling = true;
			//nma.SetDestination(transform.position);
			StartCoroutine("CoroutineForLookingAtConsole", 0.25f);
		}
	}

	public void CancelOrder()
	{
		if (myConsole != null)
        {
            myConsole.persoOnMe = false;
            myConsole.persoOnMeID = -1;
			myConsole = null;
        }
		PlayerAnim.SetBool("OnConsole", false);
		PlayerAnim.SetBool("GoRun", false);
		vaJouerUneCarte = false;
		vaRamasserUneCarte = false;
		vaSurUneConsole = false;
		isConsoling = false;
		WantedCarteId = -1;
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
        myConsole = _myConsole;
        PlayerAnim.SetBool("GoRun", true);
        vaSurUneConsole = true;
    }

    public void OrderGoPlayACard(int _cardId, ConsoleScript _myConsole)
    {
        carteID = _cardId;
        myConsole = _myConsole;
        PlayerAnim.SetBool("GoRun", true);
		vaJouerUneCarte = true;
    }

	public void OrderGoGetACard(int _wantedCardId)
	{
		WantedCarteId = _wantedCardId;
		PlayerAnim.SetBool("GoRun", true);
		vaRamasserUneCarte = true;
	}

	private void LookConsole()
	{
		Vector3 relativePos = myConsole.keyboardConsoleToLookAt.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
	}

	IEnumerator CoroutineForLookingAtConsole(float delay)
	{
		yield return new WaitForSeconds(delay);
		LookConsole();
		yield break;
	}

}
