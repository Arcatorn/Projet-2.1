using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllActions : MonoBehaviour {


	CartesManager cartesManager;
	GameMaster gameMaster;
	public Text textADroite;
	
	void Awake () {
		cartesManager = GetComponent<CartesManager>();
		gameMaster = GetComponent<GameMaster>();
	}

	IEnumerator DureeAction(monAction a)
	{
		yield return new WaitForSeconds(2);
		cartesManager.allSalles[a.salleID].salleScript.ChangeForText();
		cartesManager.allSalles[a.salleID].salleScript.SpawnCarte(a.carteID);
		a.persoScript.canReceiveOrder = true;
		yield break;
	}

	public struct monAction
	{
		public int carteID;
		public int salleID;
		public PersoScript persoScript;
	}

	public void CallAction(int carteID, int salleID, PersoScript _persoScript)
	{
		int actionId = salleID * 6 + carteID;
		monAction action = new monAction();
		action.carteID = carteID;
		action.salleID = salleID;
		action.persoScript = _persoScript;
		_persoScript.canReceiveOrder = false;
		StartCoroutine("DureeAction", action);

		string actionName;

		if (actionId == 0)
		{
			actionName = "Emettre un son d'alarme aux alentours";
			textADroite.text = actionName;
		}
		else if (actionId == 1)
		{
			actionName = "Emettre un son joyeux";
			textADroite.text = actionName;
		}
		else if (actionId == 2)
		{
			actionName = "Emettre un son intimidant";
			textADroite.text = actionName;
		}
		else if (actionId == 3)
		{
			actionName = "Proposer une destination commune";
			textADroite.text = actionName;
		}
		else if (actionId == 4)
		{
			actionName = "Jouer l'hymne du RPR";
			textADroite.text = actionName;
		}
		else if (actionId == 5)
		{
			actionName = "Proposer un échange";
			textADroite.text = actionName;
		}
		else if (actionId == 6)
		{
			actionName = "Ouvrir/Fermer les fenêtres de la salle ciblée";
			textADroite.text = actionName;
		}
		else if (actionId == 7)
		{
			actionName = "Action le bouclier";
			textADroite.text = actionName;
		}
		else if (actionId == 8)
		{
			actionName = "Activer les crampons";
			textADroite.text = actionName;
		}
		else if (actionId == 9)
		{
			actionName = "Verrouiller/Déverouiller les portes de la salle ciblée";
			textADroite.text = actionName;
		}
		else if (actionId == 10)
		{
			actionName = "Abaisser/Lever la rampe d'accès";
			textADroite.text = actionName;
		}
		else if (actionId == 11)
		{
			actionName = "Activer le brouilleur";
			textADroite.text = actionName;
		}
		else if (actionId == 12)
		{
			actionName = "Ciler le système de radar adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 13)
		{
			actionName = "...";
			textADroite.text = actionName;
		}
		else if (actionId == 14)
		{
			actionName = "Cibler un point d'ancrage dans le relief";
			textADroite.text = actionName;
		}
		else if (actionId == 15)
		{
			actionName = "Cibler le système de navigation adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 16)
		{
			actionName = "Scanner les émissions d'ondes alentours";
			textADroite.text = actionName;
		}
		else if (actionId == 17)
		{
			actionName = "Scanner les roches précieuses alentours";
			textADroite.text = actionName;
		}
		else if (actionId == 18)
		{
			actionName = "Envoyer un drone de reconnaissance";
			textADroite.text = actionName;
		}
		else if (actionId == 19)
		{
			actionName = "Purger le système d'un virus";
			textADroite.text = actionName;
		}
		else if (actionId == 20)
		{
			actionName = "Envoyer un drone saboteur";
			textADroite.text = actionName;
		}
		else if (actionId == 21)
		{
			actionName = "Piarter la rampe d'accès adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 22)
		{
			actionName = "Envoyer un drone anti-incendie (ciblage)";
			textADroite.text = actionName;
		}
		else if (actionId == 23)
		{
			actionName = "Pirater le système de communication adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 24)
		{
			actionName = "Entamer une manoeuvre évasive";
			textADroite.text = actionName;
		}
		else if (actionId == 25)
		{
			actionName = "Passer en surrégime";
			textADroite.text = actionName;
		}
		else if (actionId == 26)
		{
			actionName = "Escalader un relief proche";
			textADroite.text = actionName;
		}
		else if (actionId == 27)
		{
			actionName = "Fuir la position actuelle";
			textADroite.text = actionName;
		}
		else if (actionId == 28)
		{
			actionName = "...";
			textADroite.text = actionName;
		}
		else if (actionId == 29)
		{
			actionName = "Creuser un tunnel";
			textADroite.text = actionName;
		}
		else if (actionId == 30)
		{
			actionName = "Tirer un bloc de roche (ciblage)";
			textADroite.text = actionName;
		}
		else if (actionId == 31)
		{
			actionName = "Utiliser la foreuse devant soi";
			textADroite.text = actionName;
		}
        else if (actionId == 32)
        {
            actionName = "Tirer plusieurs blocs de roches d'un coup";
            textADroite.text = actionName;
        }
        else if (actionId == 33)
        {
            actionName = "Attirer le cable du grappin";
            textADroite.text = actionName;
        }
        else if (actionId == 34)
        {
            actionName = "Tirer/Détacher le grappin (ciblage)";
            textADroite.text = actionName;
        }
		else if (actionId == 35)
        {
            actionName = "Extraire des roches du sol";
            textADroite.text = actionName;
        }
	}

}
