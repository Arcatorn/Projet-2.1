using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllActions : MonoBehaviour {

	CartesManager cartesManager;
	GameMaster gameMaster;
	
	void Awake () {
		cartesManager = GetComponent<CartesManager>();
		gameMaster = GetComponent<GameMaster>();
	}

	IEnumerator DureeAction(monAction a)
	{
		yield return new WaitForSeconds(2);
		cartesManager.allSalles[a.salleID].salleScript.sr.sprite = Resources.Load<Sprite> ("Sprites/Cartes/Blanc");
		cartesManager.allSalles[a.salleID].salleScript.SpawnCarte(a.carteID);
		yield break;
	}

	public struct monAction
	{
		public int carteID;
		public int salleID;
	}

	public void CallAction(int carteID, int salleID)
	{
		int actionId = salleID * 6 + carteID;
		monAction action = new monAction();
		action.carteID = carteID;
		action.salleID = salleID;
		StartCoroutine("DureeAction", action);

		if (actionId == 0)
		{

		}
		else if (actionId == 1)
		{

		}
		else if (actionId == 2)
		{

		}
		else if (actionId == 3)
		{

		}
		else if (actionId == 4)
		{

		}
		else if (actionId == 5)
		{

		}
		else if (actionId == 6)
		{

		}
		else if (actionId == 7)
		{

		}
		else if (actionId == 8)
		{

		}
		else if (actionId == 9)
		{

		}
		else if (actionId == 10)
		{

		}
		else if (actionId == 11)
		{

		}
		else if (actionId == 12)
		{

		}
		else if (actionId == 13)
		{

		}
		else if (actionId == 14)
		{

		}
		else if (actionId == 15)
		{

		}
		else if (actionId == 16)
		{

		}
		else if (actionId == 17)
		{

		}
		else if (actionId == 18)
		{

		}
		else if (actionId == 19)
		{

		}
		else if (actionId == 20)
		{

		}
		else if (actionId == 21)
		{

		}
		else if (actionId == 22)
		{

		}
		else if (actionId == 23)
		{

		}
		else if (actionId == 24)
		{

		}
		else if (actionId == 25)
		{

		}
		else if (actionId == 26)
		{

		}
		else if (actionId == 27)
		{

		}
		else if (actionId == 28)
		{

		}
		else if (actionId == 29)
		{

		}
		else if (actionId == 30)
		{

		}
		else if (actionId == 31)
		{

		}
	}

}
