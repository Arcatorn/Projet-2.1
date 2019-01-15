using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CartesTypes
{
	Yeux = 1,
	Estomac = 2,
	Griffes = 3,
	Pattes = 4,
	Bouche = 5,
	Truffe = 6
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
	public Cartes(int _id, CartesTypes _cartesTypes)
	{
		id = _id;
		cartesTypes = _cartesTypes;
		illu = Resources.Load("Sprites/Cartes/Carte" + id.ToString()) as Sprite;
		// Recupere les cartes de Simon (un truc avec les gameObject)
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
	public List<Salle> allSalles = new List<Salle>();
	public static OnePlay toTransform;
	public static bool aPlayHasOccured = false;
	public GameMaster gameMaster;
	public int nouveauPlayerActif = 1;
    void Awake()
    {
		gameMaster = GetComponent<GameMaster>();
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
            Cartes c = new Cartes(i, (CartesTypes)i);
            allCards.Add(c);
            if (i < 3)
            {
                cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
                cartesButtonsScripts[i].id = i;
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
			for (int i = 0; i < 3; i++)
			{
				cartesButtonsScripts[i].gameObject.GetComponent<Image>().sprite = Instantiate(Resources.Load("Sprites/Cartes/Carte" + playerTwoCards[i].id.ToString())) as Sprite;
			}
		}
		else{
			nouveauPlayerActif = 1;
			for (int i = 0; i < 3; i++)
			{
				cartesButtonsScripts[i].gameObject.GetComponent<Image>().sprite = Instantiate(Resources.Load("Sprites/Cartes/Carte" + playerOneCards[i].id.ToString())) as Sprite;
			}
		}
	}

}

