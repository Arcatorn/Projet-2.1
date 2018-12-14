using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationModule : MonoBehaviour {

	Camera cam;
	ParticleSystem ps;
	public static GameObject module;
	
	private void Start() {
		cam = Camera.main;
		ps = GetComponent<ParticleSystem>();
		module = null;
	}

	private void Update() {
		DetectModule();
	}

	void DetectModule()
	{
		RaycastHit hit;
		Vector3 dir = transform.position - cam.transform.position;
		int layer_mask = LayerMask.GetMask("Module");
		if(Physics.Raycast(transform.position, dir.normalized, out hit, 10, layer_mask))
		{
            if (!hit.transform.gameObject.GetComponent<ModulesScript>().isActivated)
            {
                var main = ps.main;
                main.startColor = hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                module = hit.transform.gameObject;
            }
		}
		else {
			var main = ps.main;
			if (main.startColor.color != Color.white)
			{
				main.startColor = Color.white;
				module = null;
			}
		}
	}


}
