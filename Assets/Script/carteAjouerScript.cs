using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carteAjouerScript : MonoBehaviour {

	MeshRenderer meshRenderer;
	public Color baseColor;
	SalleScript salleScriptCollider;
	GameMaster gm;
	public GameObject caj;

	void Awake() {
		meshRenderer = caj.GetComponent<MeshRenderer>();
		if (gm == null)
		{
			gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
		}
	}
	private void OnEnable() {
		meshRenderer.material.color = baseColor;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Module")
		{
			meshRenderer.material.color = Color.blue;
			salleScriptCollider = other.transform.parent.GetComponent<SalleScript>();
			salleScriptCollider.OnDragCardOnMe();
			GameMaster.hittingAModule = true;
            GameMaster.moduleHit = other.transform.gameObject;
		}
	}


	private void OnTriggerExit(Collider other) {
		if (other.tag == "Module")
		{
			meshRenderer.material.color = baseColor;
			other.transform.parent.GetComponent<SalleScript>().OnExitCardOnMe();
			GameMaster.hittingAModule = false;
            GameMaster.moduleHit = null;
		}
	}

	void OnDisable()
	{
		salleScriptCollider.OnExitCardOnMe();
	}
}
