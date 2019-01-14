using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Vector3 direction;
	Rigidbody rb;
	float speed = 50;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = direction * speed;
	}
	

	void Update () {
		rb.velocity = direction * speed;
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Obstacles")
		{
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Ennemi")
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
