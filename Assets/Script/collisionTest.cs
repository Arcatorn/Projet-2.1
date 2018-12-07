using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionTest : MonoBehaviour
{
	[SerializeField] caravaneController caravaneScript;
	GameObject actualPlayer;

	void Awake()
	{
		actualPlayer = this.gameObject;
	}
}
