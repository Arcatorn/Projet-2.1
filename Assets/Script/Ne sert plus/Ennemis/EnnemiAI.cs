using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiAI : MonoBehaviour {

	public GameObject caravane;
	NavMeshAgent nav;

	private void Awake() {
		if (!caravane)
		{
			caravane = GameObject.Find("Caravane");
		}
		nav = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		var distanceToCaravane = Vector3.Distance(transform.position, caravane.transform.position);

		if (distanceToCaravane <= 200)
		{
			nav.SetDestination(caravane.transform.position);
		}
	}




}
