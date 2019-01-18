using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeGestion : MonoBehaviour {

    public Camera MainCamera;
    public CameraShake CamScript;
	// Use this for initialization
	void Start ()
    {
    	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKey(KeyCode.D))
        {
            CamScript.shakeDuration = 10;
        }
        if (Input.GetKey(KeyCode.F))
        {

        }
        if (Input.GetKey(KeyCode.G))
        {

        }
    }
}
