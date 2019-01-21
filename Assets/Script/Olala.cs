using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olala : MonoBehaviour {

public GameObject d;
public GameObject bombe;
public GameObject explosion;
public GameObject feuuuuuuu;
public GameObject ecran;

	public void GoDroneIncoming()
	{
		d.SetActive(true);
		StartCoroutine("WaitForDroneAnimation",6f);
	}

	IEnumerator WaitForDroneAnimation(float delay)
	{
		yield return new WaitForSeconds(10);
		Destroy(d);
		Destroy(bombe);
		explosion.SetActive(true);
		feuuuuuuu.SetActive(true);
		ecran.SetActive(false);
		yield break;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A))
		{
			GoDroneIncoming();
		}
	}

}
