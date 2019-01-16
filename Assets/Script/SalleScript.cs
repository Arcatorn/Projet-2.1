using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SalleScript : MonoBehaviour {

	public Material normalMaterial;
	public Material onCardDrag;
	MeshRenderer mr;
	public int id;
	public SpriteRenderer sr;
	public GameObject[] spotCartes = new GameObject[3];
	public bool[] spotInUse = new bool[3];
	CartesManager cartesManager;

	void Awake () 
	{
		if (mr == null)
		{
			mr = GetComponent<MeshRenderer>();
			mr.material = normalMaterial;
		}
		cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
		for (int i = 0; i < spotInUse.Length; i++)
		{
			spotInUse[i] = false;
		}
	}

	public void OnDragCardOnMe()
	{
		mr.material = onCardDrag;
	}

	public void OnExitCardOnMe()
	{
		mr.material = normalMaterial;
	}

    public void SpawnCarte(int carteID)
    {
        int rnd = Random.Range(0, 3);
        if (!spotInUse[rnd])
        {
            cartesManager.cartesPhysiques[carteID].transform.position = spotCartes[rnd].transform.position;
			cartesManager.cartesPhysiques[carteID].GetComponent<CartePhysiqueScript>().salleID = id;
			cartesManager.cartesPhysiques[carteID].GetComponent<CartePhysiqueScript>().spotID = rnd;
			spotInUse[rnd] = true;
        }
        else
        {
            if (!TestSpot())
            {
                SpawnCarte(carteID);
            }
        }
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
}
