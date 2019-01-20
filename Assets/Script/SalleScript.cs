using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SalleScript : MonoBehaviour {

	public Material normalMaterial;
	public Material onCardDrag;
	[SerializeField] MeshRenderer mr;
	public int id;
	public SpriteRenderer sr;
	public GameObject[] spotCartes = new GameObject[3];
	public bool[] spotInUse = new bool[3];
	CartesManager cartesManager;
	public SpriteRenderer textConsole;
	public ConsoleScript myConsoleScript;
	public GameObject myModule;
    public bool isFull = false;

    void Awake()
    {

        mr.material = onCardDrag;

        cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
        for (int i = 0; i < spotInUse.Length; i++)
        {
            spotInUse[i] = false;
        }
        myConsoleScript = GetComponentInChildren<ConsoleScript>();
    }

	public void OnDragCardOnMe()
	{
		mr.material = normalMaterial;
		if(mr.gameObject.transform.childCount > 0)
		{
			mr.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = normalMaterial;
		}
	}

	public void OnExitCardOnMe()
	{
		mr.material = onCardDrag;
		if(mr.gameObject.transform.childCount > 0)
		{
			mr.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = onCardDrag;
		}
	}

    /*public void SpawnCarte(int carteID)
    {
        int rnd = Random.Range(0, 3);
        if (!spotInUse[rnd])
        {
            cartesManager.cartesPhysiques[carteID].transform.position = spotCartes[rnd].transform.position;
            CartePhysiqueScript cartePhysiqueScript = cartesManager.cartesPhysiques[carteID].GetComponent<CartePhysiqueScript>();
			cartePhysiqueScript.salleID = id;
			cartePhysiqueScript.spotID = rnd;
            cartePhysiqueScript.canBeInteract = true;
			spotInUse[rnd] = true;
        }
        else
        {
            if (!TestSpot())
            {
                SpawnCarte(carteID);
            }
        }
    }*/

    public void FindRandomSpot(int carteID)
    {
        int rnd = Random.Range(0, 3);
        if (!spotInUse[rnd])
        {
            Vector3 pos = spotCartes[rnd].transform.position;
            pos.y = -2.6f;
            cartesManager.cartesPhysiques[carteID].transform.position = pos;
            CartePhysiqueScript cartePhysiqueScript = cartesManager.cartesPhysiques[carteID].GetComponent<CartePhysiqueScript>();
            cartePhysiqueScript.salleID = id;
			cartePhysiqueScript.spotID = rnd;
            spotCartes[rnd].GetComponentInChildren<Animator>().SetTrigger("Ouvre");
            spotInUse[rnd] = true;
            StartCoroutine("WaitOuverture", cartePhysiqueScript);
        }
        else{
            
            if (!TestSpot())
            {
                FindRandomSpot(carteID);
            }
        }
    }

    IEnumerator WaitOuverture(CartePhysiqueScript cartePhysiqueScript)
    {
        yield return new WaitForSeconds(1);
        cartePhysiqueScript.StartCoroutine("MonterLeCube");
        yield break;
    }


    bool TestSpot()
    {
        bool toReturn = true;
        for (int i = 0; i < spotInUse.Length; i++)
        {
            if (spotInUse[i] == false)
            {
                toReturn = false;
            }
        }
        return toReturn;
    }

	public void ChangeForPicto(Sprite _picto)
	{
		sr.sprite = _picto;
		textConsole.enabled = false;
	}

	public void ChangeForText()
	{
		sr.sprite = null;
		textConsole.enabled = true;
	}

    public void CheckMaterial()
    {
        if (mr.material.name.Contains(normalMaterial.name))
        {
            if (GameMaster.moduleHit == null)
            {
                OnExitCardOnMe();
            }
            else
            {
                if (GameMaster.moduleHit != myModule)
                {
                    OnExitCardOnMe();
                }
            }
        }
    }

	void Update()
	{
		CheckMaterial();
        CheckIsFull();
	}

    public void CheckIsFull()
    {
        isFull = true;
        for (int i = 0; i < 3; i++)
        {
            if (spotInUse[i] == false)
            {
                isFull = false;
            }
        }
    }
}
