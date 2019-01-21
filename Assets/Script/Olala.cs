using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olala : MonoBehaviour {

public GameObject d;
public GameObject bombe;
public GameObject explosion;
public GameObject feuuuuuuu;
public GameObject ecran;
public GameObject ragnaros;
public List<Transform>  allChilds = new List<Transform>();

private void Awake() {
	foreach(Transform child in ragnaros.transform)
	{
		allChilds.Add(child);
	}
}

	public void GoDroneIncoming()
	{
		d.SetActive(true);
		StartCoroutine("WaitForDroneAnimation",6f);
	}

	IEnumerator WaitForDroneAnimation(float delay)
	{
		GameObject.Find("BombSound").GetComponent<BombeSound>().Travel();
		yield return new WaitForSeconds(3.5f);
		GameObject.Find("BombSound").GetComponent<BombeSound>().Bombe();
		yield return new WaitForSeconds(6);
		Destroy(d);
		Destroy(bombe);
		explosion.SetActive(true);
		feuuuuuuu.SetActive(true);
		ecran.SetActive(false);
		StartCoroutine("Propagation", 1);
		GameObject.Find("Ragnaros").GetComponent<Flamepropagation>().Feu();
		yield break;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A))
		{
			GoDroneIncoming();
		}
	}

	IEnumerator Propagation(int id)
	{
		yield return new WaitForSeconds(1);
		allChilds[id].gameObject.SetActive(true);
		if (id < allChilds.Count - 1)
		{
			StartCoroutine("Propagation", id+1);
		}
		yield break;
		
	}

}
