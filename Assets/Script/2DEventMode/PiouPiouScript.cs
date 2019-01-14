using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiouPiouScript : MonoBehaviour {

	Rigidbody2D rb;
	public Vector3 direction;
	public float speed = 3f;
	public GameObject ennemi;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = direction * speed;
		StartCoroutine("DestroyTir");
	}

	void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject == ennemi)
        {
            other.gameObject.GetComponent<CaravanesScriptsAuto>().hasBeenHit = true;
            Destroy(gameObject);
        }
	}

	IEnumerator DestroyTir()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
		yield break;
	}
}
