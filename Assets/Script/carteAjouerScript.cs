using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carteAjouerScript : MonoBehaviour {

	MeshRenderer meshRenderer;
	public Color baseColor;

	void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
	}
	private void OnEnable() {
		meshRenderer.material.color = baseColor;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Module")
		{
			meshRenderer.material.color = other.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Module")
		{
			meshRenderer.material.color = baseColor;
		}
	}
}
