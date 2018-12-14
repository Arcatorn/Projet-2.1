using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiouPiouScript : MonoBehaviour {

	Rigidbody2D rb;
	public Vector3 direction;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = direction * 3;
	}

	void Update()
	{
		if (transform.position.magnitude > 20)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		other.gameObject.GetComponent<CaravanesScriptsAuto>().hasBeenHit = true;
		Destroy(gameObject);
	}
}
