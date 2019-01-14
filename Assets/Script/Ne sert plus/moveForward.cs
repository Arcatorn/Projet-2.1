using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour
{
	public testNM2 master;

    void Awake()
    {
        if (master == null && GameObject.Find("Caravane Qui Bouge") != null)
        {
            master = GameObject.Find("Caravane Qui Bouge").GetComponent<testNM2>();
        }
    }
    void OnTriggerEnter()
    {
        master.DirectionController();
    }
}