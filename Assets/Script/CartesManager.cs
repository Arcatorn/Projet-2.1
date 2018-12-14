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

public enum moduleType
{
	vert = 0,
	bleu = 1,
	rouge = 2
}
public class Cartes
{
	public CartesTypes type;

	virtual public void Action(moduleType t){}
}

public class Deplacement : Cartes
{

	effets effetVert;
	effets effetBleu;
	effets effetRouge;

	public Deplacement()
	{
		type = CartesTypes.Deplacement;
		effetBleu = new DeplacementBleu();
		effetRouge = new DeplacementRouge();
		effetVert = new DeplacementVert();
	}

	override public void Action (moduleType t)
	{
		if (t == moduleType.vert)
		{
			effetVert.EffectuerAction();
		}
		else if (t == moduleType.rouge)
		{
			effetBleu.EffectuerAction();
		}
		else if (t == moduleType.bleu)
		{
			effetRouge.EffectuerAction();
		}
	}
}

public class Armement : Cartes
{
	effets effetVert;
	effets effetBleu;
	effets effetRouge;

	public Armement()
	{
		type = CartesTypes.Armement;
		effetBleu = new ArmementBleu();
		effetRouge = new ArmementRouge();
		effetVert = new ArmementVert();
	}

	override public void Action (moduleType t)
	{
		if (t == moduleType.vert)
		{
			effetVert.EffectuerAction();
		}
		else if (t == moduleType.rouge)
		{
			effetBleu.EffectuerAction();
		}
		else if (t == moduleType.bleu)
		{
			effetRouge.EffectuerAction();
		}
	}
}

public class CartesManager : MonoBehaviour {

	public List<Cartes> playerCards = new List<Cartes>();
	public Image[] emplacements;
	CartesButtons[] cartesButtonsScripts = new CartesButtons[3];
	public testNM2 master;
	public static OnePlay toTransform;
	public List<OnePlay2> isExecuting = new List<OnePlay2>();
	public static bool aPlayHasOccured = false;

    void Awake()
    {
        if (master == null && GameObject.Find("Caravane") != null)
        {
            master = GameObject.Find("Caravane").GetComponent<testNM2>();
        }
    }
	
	void Start () {
		Initialize();
	}
	
	
	void Update () {
		if (aPlayHasOccured)
		{
			TransformPlay();
		}
		GestionAllPlay();
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
            else
            {
                Cartes c = new Armement();
                playerCards.Add(c);
                cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
                cartesButtonsScripts[i].id = i;
            }
        }
    }

    public struct OnePlay
	{
		public int cardID;
		public GameObject mGO;
	}

	public struct OnePlay2
	{
		public Cartes c;
		public moduleType m;
		public ModulesScript ms;
	}
	public void PlayACardOnModule(int id)
	{
		OnePlay o = new OnePlay();
		o.cardID = id;
		o.mGO = GameMaster.moduleHit;
		toTransform = o;
		aPlayHasOccured = true;
	}

	void TransformPlay()
    {
        OnePlay2 o = new OnePlay2();
        o.c = playerCards[toTransform.cardID];
        o.ms = toTransform.mGO.GetComponent<ModulesScript>();
        o.m = o.ms.myType;
        o.ms.isActivated = true;
        isExecuting.Add(o);
        o.c.Action(o.m);
        aPlayHasOccured = false;
    }

	void GestionAllPlay()
	{
        for (int i = 0; i < isExecuting.Count; i++)
        {
            if (!isExecuting[i].ms.isActivated)
            {
				isExecuting.Remove(isExecuting[i]);
            }
        }
	}


}

public class effets
{
	virtual public void EffectuerAction()
    {
        
	}
}

public class DeplacementVert : effets
{
	testNM2 caravaneScript;

	public DeplacementVert()
	{
		caravaneScript = GameObject.Find("Caravane").GetComponent<testNM2>();
	}
	override public void EffectuerAction()
    {
        caravaneScript.DirectionController();
	}
}

public class DeplacementBleu : effets
{
	testNM2 caravaneScript;
	public DeplacementBleu()
	{
		caravaneScript = GameObject.Find("Caravane").GetComponent<testNM2>();
	}
	override public void EffectuerAction()
    {
        
	}
}

public class DeplacementRouge : effets
{
	testNM2 caravaneScript;
	public DeplacementRouge()
	{
		caravaneScript = GameObject.Find("Caravane").GetComponent<testNM2>();
	}
	override public void EffectuerAction()
    {
        
	}
}

public class ArmementVert : effets
{
	override public void EffectuerAction()
	{
		Tourelle.mustShoot = true;
	}
}
public class ArmementBleu : effets
{
	override public void EffectuerAction()
	{
		Tourelle.mustShoot = true;
	}
}

public class ArmementRouge : effets
{
	override public void EffectuerAction()
	{
		Tourelle.mustShoot = true;
	}
}