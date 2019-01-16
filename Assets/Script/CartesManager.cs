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
	public Cartes(int _id, CartesTypes _cartesTypes, GameObject _cartePhysique)
	{
		id = _id;
		cartesTypes = _cartesTypes;
		cartePhysique = _cartePhysique;
		illu = Resources.Load <Sprite> ("Sprites/Cartes/Carte" + id.ToString());
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
	public int nouveauPlayerActif = 1;
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
		toTransform = o;
		aPlayHasOccured = true;
	}

	void TransformPlay()
    {
		int idSalle = toTransform.mGO.GetComponent<SalleScript>().id;
		allSalles[idSalle].salleScript.sr.sprite = cartesButtonsScripts[toTransform.cardID].gameObject.GetComponent<Image>().sprite;
		allActions.CallAction(toTransform.cardID, idSalle);
		if (nouveauPlayerActif == 1)
		{
			playerOneCards.Remove(allCards[toTransform.cardID]);
			SortCartes(playerOneCards);
		}
		else
		{
			playerTwoCards.Remove(allCards[toTransform.cardID]);
			SortCartes(playerTwoCards);
		}
		
        aPlayHasOccured = false;
    }

	void InitializeSalles()
	{
		for (int i = 0; i < 3; i++)
		{
			Salle a = new Salle(GameObject.Find("Salle" + i.ToString()), (SallesTypes)i);
			allSalles.Add(a);
			a.salleScript.id = i;
		}
	}

	public void ChangerPictoMainDuJoueur()
	{
		if (nouveauPlayerActif == 1)
		{
			nouveauPlayerActif = 2;
			SortCartes(playerTwoCards);
		}
		else{
			nouveauPlayerActif = 1;
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
			}
			else {
				m.sprite = Resources.Load<Sprite> ("Sprites/Cartes/Blanc");
				m.enabled = false;
			}
		}
	}

}

