﻿using System.Collections;
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
		yield return new WaitForSeconds(1);
		cartesManager.allSalles[a.salleID].salleScript.FindRandomSpot(a.carteID);
		yield return new WaitForSeconds(1);
		cartesManager.allSalles[a.salleID].salleScript.ChangeForText();
		
		cartesManager.SortCartesIndicators();
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
			// RADAR + OEIL
			actionName = "Ciler le système de radar adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 1)
		{
			// RADAR + ESTOMAC
			actionName = "...";
			textADroite.text = actionName;
		}
		else if (actionId == 2)
		{
			// RADAR + GRIFFES
			actionName = "Cibler un point d'ancrage dans le relief";
			textADroite.text = actionName;
		}
		else if (actionId == 3)
		{
			// RADAR + PATTES
			actionName = "Cibler le système de navigation adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 4)
		{
			// RADAR + GUEULE
			actionName = "Scanner les émissions d'ondes alentours";
			textADroite.text = actionName;
		}
		else if (actionId == 5)
		{
			// RADAR + TRUFFE
			actionName = "Scanner les roches précieuses aux alentours";
			textADroite.text = actionName;
		}
		else if (actionId == 6)
		{
			// ELEC + OEIL
			actionName = "Envoyer un dron de reconnaissance";
			textADroite.text = actionName;
		}
		else if (actionId == 7)
		{
			// ELEC + ESTOMAC
			actionName = "Purger le système d'un virus";
			textADroite.text = actionName;
		}
		else if (actionId == 8)
		{
			// ELEC + Griffes
			actionName = "Envoyer un drone saboteur";
			textADroite.text = actionName;
		}
		else if (actionId == 9)
		{
			// ELEC + PATTES
			actionName = "Pirater la rampe d'accès adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 10)
		{
			// ELEC + GUEULE
			actionName = "Envoyer un drone anti-incendie";
			textADroite.text = actionName;
		}
		else if (actionId == 11)
		{
			// ELEC + TRUFFE
			actionName = "Pirater le système de communication adverse";
			textADroite.text = actionName;
		}
		else if (actionId == 12)
		{
			// NAV + OEIL
			actionName = "Déclencher une manoeuvre évasive";
			textADroite.text = actionName;
		}
		else if (actionId == 13)
		{
			// NAV + ESTOMAC
			actionName = "Passer en surrégime";
			textADroite.text = actionName;
		}
		else if (actionId == 14)
		{
			// NAV + GRIFFES
			actionName = "Escalader un relief proche";
			textADroite.text = actionName;
		}
		else if (actionId == 15)
		{
			// NAV + PATTES
			actionName = "Fuir";
			textADroite.text = actionName;
		}
		else if (actionId == 16)
		{
			// NAV + GUEULE
			actionName = "...";
			textADroite.text = actionName;
		}
		else if (actionId == 17)
		{
			// NAV + TRUFFE
			actionName = "Creuser un tunnel";
			textADroite.text = actionName;
		}
		else if (actionId == 18)
		{
			// DEF + OEIL
			actionName = "Ouvrir/Fermer les fenêtres de la salle ciblée";
			textADroite.text = actionName;
		}
		else if (actionId == 19)
		{
			// DEF + ESTOMAC
			actionName = "Activer le bouclier";
			textADroite.text = actionName;
		}
		else if (actionId == 20)
		{
			// DEF + GRIFFES
			actionName = "Activer les crampons";
			textADroite.text = actionName;
		}
		else if (actionId == 21)
		{
			// DEF + PATTES
			actionName = "Verrouiller/Déverouiller les portes de la salle ciblée";
			textADroite.text = actionName;
		}
		else if (actionId == 22)
		{
			// DEF + GUEULE
			actionName = "Abaisser/Lever la rampe d'accès";
			textADroite.text = actionName;
		}
		else if (actionId == 23)
		{
			// DEF + TRUFFE
			actionName = "Activer le brouilleur";
			textADroite.text = actionName;
		}
		else if (actionId == 24)
		{
			// EXCA + OEIL
			actionName = "Tirer un bloc de roche en utilisant le ciblage";
			textADroite.text = actionName;
		}
		else if (actionId == 25)
		{
			// EXCA + ESTOMAC
			actionName = "Utiliser la foreuse devant soi";
			textADroite.text = actionName;
		}
		else if (actionId == 26)
		{
			// EXCA + GRIFFES
			actionName = "Tirer 3 blocs de roches d'un coup";
			textADroite.text = actionName;
		}
		else if (actionId == 27)
		{
			// EXCA + PATTES
			actionName = "Attirer le câble du grappin";
			textADroite.text = actionName;
		}
		else if (actionId == 28)
		{
			// EXCA + GUEULE
			actionName = "Lancer le grappin";
			textADroite.text = actionName;
		}
		else if (actionId == 29)
		{
			// EXCA + TRUFFE
			actionName = "Extraire les roches du sol environnant";
			textADroite.text = actionName;
		}
		else if (actionId == 30)
		{
			// COM + OEIL
			actionName = "Emettre un son d'alarme";
			textADroite.text = actionName;
		}
		else if (actionId == 31)
		{
			// COM + ESTOMAC
			actionName = "Emettre un son joyeux";
			textADroite.text = actionName;
		}
        else if (actionId == 32)
        {
			// COM + GRIFFES
            actionName = "Emettre un son intimidant";
            textADroite.text = actionName;
        }
        else if (actionId == 33)
        {
			// COM + PATTES
            actionName = "Proposer une destination commune";
            textADroite.text = actionName;
        }
        else if (actionId == 34)
        {
			// COM + GUEULE
            actionName = "Jouer l'hymne du RPR";
            textADroite.text = actionName;
        }
		else if (actionId == 35)
        {
			// COM + TRUFFE
            actionName = "Proposer un échange";
            textADroite.text = actionName;
        }
	}

}
