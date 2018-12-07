using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionTest : MonoBehaviour
{
	[SerializeField] caravaneController caravaneScript;
	void OnTriggerEnter(Collider col)
	{
		caravaneScript.detectButton(col.transform.gameObject);
		print("touched");
	}
}
