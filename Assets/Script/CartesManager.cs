using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CartesTypes
{
	Deplacement = 0,
	Radar = 1,
	Armement = 2,
	Ramasser = 3,
	Terraformer = 4,
	Reperation = 5
}



public abstract class Cartes
{
    public CartesTypes type;

    virtual public void Action(MyCaravaneScript m) { }
}

public class Deplacement : Cartes
{
    public Deplacement()
    {
        type = CartesTypes.Deplacement;
    }

    override public void Action(MyCaravaneScript m)
    {
		Debug.Log("Esquive !");
    }
}

public class Armement : Cartes
{
    public Armement()
    {
        type = CartesTypes.Armement;
    }

    override public void Action(MyCaravaneScript m)
    {
		Debug.Log("Tir !");
		m.PiouPiou();
    }
}

public class Brouillage : Cartes
{
    public Brouillage()
    {
        type = CartesTypes.Radar;
    }

    override public void Action(MyCaravaneScript m)
    {
		Debug.Log("Brouillage !");
    }
}

public class Salle
{
    public GameObject salleGO;
    public SalleScript salleScript;
    public int cd;
	public Cartes myCarte;
	public float timer;
	public MyCaravaneScript m;

    public Salle(GameObject _salleGO, int _cd, MyCaravaneScript _mm)
    {
        salleGO = _salleGO;
        salleScript = _salleGO.GetComponent<SalleScript>();
        cd = _cd;
		m =_mm;
		RemoveCarte();
    }

	public void RemoveCarte()
	{
		myCarte = null;
	}
	public void AddCarte(Cartes c)
	{
		myCarte = c;
	}

    public void CoolDown(float t)
    {
        timer += t;
        if (timer >= cd)
        {
            if (myCarte != null)
            {
                ExecuteAction();
            }
            timer = 0;
        }
    }

	public void ExecuteAction()
	{
		myCarte.Action(m);
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

	public MyCaravaneScript myCaravaneScript;

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
		GestionAllSalles();
	}

    void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                Cartes c = new Deplacement();
                playerCards.Add(c);
                cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
                cartesButtonsScripts[i].id = i;
            }
            else if (i == 1)
            {
                Cartes c = new Brouillage();
                playerCards.Add(c);
                cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
                cartesButtonsScripts[i].id = i;
            }
			else
			{
				Cartes c = new Armement();
                playerCards.Add(c);
                cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
                cartesButtonsScripts[i].id = i;
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
		Debug.Log(idSalle);
		allSalles[idSalle].AddCarte(playerCards[toTransform.cardID]);
		allSalles[idSalle].salleScript.sr.sprite = cartesButtonsScripts[toTransform.cardID].gameObject.GetComponent<Image>().sprite;
        aPlayHasOccured = false;
    }

	void GestionAllSalles()
	{
       for (int i = 0; i < allSalles.Count; i++)
	   {
		   allSalles[i].CoolDown(Time.deltaTime);
	   }
	}

	void InitializeSalles()
	{
		Salle a = new Salle(GameObject.Find("Salle1"), 3, myCaravaneScript);
		allSalles.Add(a);
		a.salleScript.id = 0;
		a = new Salle(GameObject.Find("Salle2"), 5, myCaravaneScript);
		allSalles.Add(a);
		a.salleScript.id = 1;
		a = new Salle(GameObject.Find("Salle3"), 8, myCaravaneScript);
		allSalles.Add(a);
		a.salleScript.id = 2;
	}

}

