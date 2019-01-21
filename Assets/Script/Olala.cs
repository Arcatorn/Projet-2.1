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
		gameObject.GetComponent<BombeSound>().Travel();
		yield return new WaitForSeconds(3.5f);
		gameObject.GetComponent<BombeSound>().Bombe();
		yield return new WaitForSeconds(6);
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
