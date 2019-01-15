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

	public Cartes(int _id, CartesTypes _cartesTypes)
	{
		id = _id;
		cartesTypes = _cartesTypes;
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

	public List<Cartes> playerCards = new List<Cartes>();
	public Image[] emplacements;
	CartesButtons[] cartesButtonsScripts = new CartesButtons[3];
	public List<Salle> allSalles = new List<Salle>();
	public static OnePlay toTransform;
	public static bool aPlayHasOccured = false;
	public GameMaster gameMaster;
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
        for (int i = 0; i < 3; i++)
        {
            Cartes c = new Cartes(i, (CartesTypes)i);
            playerCards.Add(c);
            cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
            cartesButtonsScripts[i].id = i;
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
		//allSalles[idSalle].AddCarte(playerCards[toTransform.cardID]);
		allSalles[idSalle].salleScript.sr.sprite = cartesButtonsScripts[toTransform.cardID].gameObject.GetComponent<Image>().sprite;
        aPlayHasOccured = false;
    }

	void InitializeSalles()
	{
		for (int i = 1; i < 4; i++)
		{
			Salle a = new Salle(GameObject.Find("Salle" + i.ToString()), (SallesTypes)i);
			allSalles.Add(a);
			a.salleScript.id = i;
		}
	}

}

