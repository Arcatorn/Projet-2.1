using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechniqueTresAncienne : MonoBehaviour {

	public GameObject sphere;

	private void OnDisable() {
		sphere.SetActive(true);
	}
}
