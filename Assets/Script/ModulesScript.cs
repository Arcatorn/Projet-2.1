using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesScript : MonoBehaviour {

	public bool isActivated = false;
	public float timerMax = 5;
	float timer = 0;
	public moduleType myType;

	void Start () {
		
	}
	

	void Update () {
		if (isActivated)
		{
			ModuleCoolDown();
		}
	}

	public void ModuleCoolDown()
	{
		timer += Time.deltaTime;

		if (timer >= timerMax)
		{
			timer = 0;
			isActivated = false;
			GetComponent<MeshRenderer>().material = Resources.Load("Materials/blue") as Material;
		}
	}
}
