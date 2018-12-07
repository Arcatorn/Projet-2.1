using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateLeft : MonoBehaviour
{
    caravaneController master;

    void Awake()
    {
        master = GameObject.Find("Caravane Qui Bouge").GetComponent<caravaneController>();
    }
    void OnTriggerStay()
    {
        master.rotateLeft();
    }
}
