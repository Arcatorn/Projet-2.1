using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    Vector3 offset;
    GameObject master;
	public GameObject objectWanted;

    public void Start()
    {
        objectWanted = GameObject.Find("Jacques, le perso bleu");
        offset = objectWanted.transform.position - transform.position;
    }

    public void selectObjectToLook(GameObject wantedObject)
    {
        objectWanted = wantedObject;
    }
    
    void Update()
    {
		transform.position = objectWanted.transform.position - offset;
    }
}
