using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CartesTypes
{
	Yeux = 0,
	Estomac = 1,
	Griffes = 2,
	Pattes = 3,
	Bouche = 4,
	Truffe = 5
}

public enum SallesTypes
{
	Excavation = 0,
	Defense = 1,
	Navigation = 2,
	Radar = 3,
	Electronics = 4,
	Communication = 5
}

public class Cartes
{
	public int id;
	public CartesTypes cartesTypes;
	public GameObject cartePhysique;
	public Sprite illu;
	public Sprite picto;
	public Cartes(int _id, CartesTypes _cartesTypes, GameObject _cartePhysique)
	{
		id = _id;
		cartesTypes = _cartesTypes;
		cartePhysique = _cartePhysique;
		illu = Resources.Load <Sprite> ("Sprites/Cartes/Carte" + id.ToString());
		picto = Resources.Load <Sprite> ("Sprites/Cartes/Picto" + id.ToString());
	}
}

public class Salle
{
	public SallesTypes sallesTypes;
    public GameObject salleGO;
    public SalleScript salleScript;
    public Salle(GameObject _salleGO, SallesTypes _sallesTypes)
    {
        salleGO = _salleGO;
        salleScript = _salleGO.GetComponent<SalleScript>();
		sallesTypes = _sallesTypes;
    }
}

public class CartesManager : MonoBehaviour {

	public List<Cartes> allCards = new List<Cartes>();
	public List<Cartes> playerOneCards = new List<Cartes>();
	public List<Cartes> playerTwoCards = new List<Cartes>();
	public Image[] emplacements;
	CartesButtons[] cartesButtonsScripts = new CartesButtons[3];
	public GameObject[] cartesPhysiques = new GameObject[6];
	public List<Salle> allSalles = new List<Salle>();
	public static OnePlay toTransform;
	public static bool aPlayHasOccured = false;
	public GameMaster gameMaster;
	public AllActions allActions;
	public int nouveauPlayerActif = 0;
    void Awake()
    {
		gameMaster = GetComponent<GameMaster>();
		allActions = GetComponent<AllActions>();
    }
	
	void Start () {
		Initialize();
	}
	
	
	void Update () {
		if (aPlayHasOccured)
		{
			TransformPlay();
		}
	}

    void Initialize()
    {
        for (int i = 0; i < 6; i++)
        {
            Cartes c = new Cartes(i, (CartesTypes)i, cartesPhysiques[i]);
            allCards.Add(c);
            if (i < 3)
            {
                cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
                cartesButtonsScripts[i].id = i;
				emplacements[i].GetComponent<Image>().sprite = c.illu;
                playerOneCards.Add(c);
            }
            else
            {
                playerTwoCards.Add(c);
            }
        }
		SortCartes(playerOneCards);
        InitializeSalles();
    }

    public struct OnePlay
	{
		public int cardID;
		public GameObject mGO;
	}

	public void PlayACardOnModule(int id)
	{
		OnePlay o = new OnePlay();
		o.cardID = id;
		o.mGO = GameMaster.moduleHit.transform.parent.gameObject;
		o.mGO.transform.GetChild(0).GetComponent<ConsoleSound>().PlayCard();
		toTransform = o;
		aPlayHasOccured = true;
	}

	void TransformPlay()
    {
		int idSalle = toTransform.mGO.GetComponent<SalleScript>().id;
		
		if (nouveauPlayerActif == 0)
		{
			allSalles[idSalle].salleScript.sr.sprite = playerOneCards[toTransform.cardID].picto;
			allActions.CallAction(playerOneCards[toTransform.cardID].id, idSalle);
			playerOneCards.RemoveAt(toTransform.cardID);
			SortCartes(playerOneCards);
		}
		else
		{
			allSalles[idSalle].salleScript.sr.sprite = playerTwoCards[toTransform.cardID].picto;
			allActions.CallAction(playerTwoCards[toTransform.cardID].id, idSalle);
			playerTwoCards.RemoveAt(toTransform.cardID);
			SortCartes(playerTwoCards);
		}
		
        aPlayHasOccured = false;
    }

	void InitializeSalles()
	{
		for (int i = 0; i < 6; i++)
		{
			Salle a = new Salle(GameObject.Find("Salle" + i.ToString()), (SallesTypes)i);
			allSalles.Add(a);
			a.salleScript.id = i;
		}
	}

	public void ChangerPictoMainDuJoueur()
	{
		if (nouveauPlayerActif == 0)
		{
			nouveauPlayerActif = 1;
			SortCartes(playerTwoCards);
		}
		else{
			nouveauPlayerActif = 0;
			SortCartes(playerOneCards);
		}
	}

	void SortCartes(List<Cartes> liste)
	{
		for (int i = 0; i < 3; i++)
		{
			Image m = cartesButtonsScripts[i].gameObject.GetComponent<Image>();
			if (i < liste.Count)
			{
				m.enabled = true;
				m.sprite = liste[i].illu;
				cartesButtonsScripts[i].textMeshProComponent.SetText(ConvertisseurIntEnChiffreRomain(liste[i].id + 1));
			}
			else {
				m.sprite = Resources.Load<Sprite> ("Sprites/Cartes/Blanc");
				m.enabled = false;
			}
		}
	}

	public bool CheckHandisFull(int player)
	{
		bool toReturn = false;
		if (player == 0)
		{
			if (playerOneCards.Count == 3)
			{
				
				toReturn = true;
			}
		}
		else{
			if (playerTwoCards.Count == 3)
			{
				toReturn = true;
			}
		}
		
		return toReturn;
	}

	public void AjouterUneCarteDansLaMain(int player, int carteId)
	{
		if (player == 0)
		{
			playerOneCards.Add(allCards[carteId]);
			SortCartes(playerOneCards);
			cartesPhysiques[carteId].transform.position = new Vector3(-100, 0,0);
			CartePhysiqueScript s = cartesPhysiques[carteId].GetComponent<CartePhysiqueScript>();
			allSalles[s.salleID].salleScript.spotInUse[s.spotID] = false;
		}
		else{
			playerTwoCards.Add(allCards[carteId]);
			SortCartes(playerTwoCards);
			cartesPhysiques[carteId].transform.position = new Vector3(-100, 0,0);
			CartePhysiqueScript s = cartesPhysiques[carteId].GetComponent<CartePhysiqueScript>();
			allSalles[s.salleID].salleScript.spotInUse[s.spotID] = false;
		}
	}

	string ConvertisseurIntEnChiffreRomain(int toConvert)
	{
		string toReturn = "O";
		if (toConvert == 1)
		{
			toReturn = "I";
		}else if (toConvert == 2)
		{
			toReturn = "II";
		}
		else if (toConvert == 3)
		{
			toReturn = "III";
		}
		else if (toConvert == 4)
		{
			toReturn = "IV";
		}
		else if (toConvert == 5)
		{
			toReturn = "V";
		}
		else if (toConvert == 6)
		{
			toReturn = "VI";
		}
		return toReturn;
	}

}

