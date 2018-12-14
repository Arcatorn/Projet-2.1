using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaravanesScriptsAuto : MonoBehaviour {

	Vector3 startPosition;
	Vector3 destination;
	public AnimationCurve ac;
	float speed = 5;
	public float distanceToMove;
	Vector3 lastPosition;
	public GameObject tir;
	public GameObject otherCaravane;
	public Image pv;
	public bool hasBeenHit = false;


	void Start () {
		startPosition = transform.position;
		destination = startPosition;
	}
	
	
	void Update () {
		if (transform.position == destination)
		{
			destination = FindNewDestination();
			distanceToMove = Vector3.Distance(transform.position, destination);
			lastPosition = transform.position;
		}
		else
		{
			MoveToDestination();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			PiouPiou();
		}
		if (hasBeenHit)
		{
			if (pv.fillAmount >= 0.1f)
			{
				pv.fillAmount -=0.1f;
				hasBeenHit = false;
			}
			else {
				Destroy(gameObject);
			}
		}
	}

	Vector3 FindNewDestination()
	{
		float x = startPosition.x + Random.Range(-3.5f, 3.5f);
		float y = startPosition.y + Random.Range(-3.5f, 3.5f);
		return new Vector3(x, y, 0);
	}

    void MoveToDestination()
    {
        float pourcentageDone = (Vector3.Distance(transform.position, lastPosition) / distanceToMove);
        if (pourcentageDone <= 0.85f)
        {
            float speedModifier = ac.Evaluate(pourcentageDone + Random.value / 2) * Time.deltaTime * speed;
			float newX = Mathf.Lerp(transform.position.x, destination.x, speedModifier);
			float newY = Mathf.Lerp(transform.position.y, destination.y, speedModifier);
			transform.position = new Vector3(newX, newY, 0);
        }
        else
        {
            transform.position = destination;
        }
    }

	void PiouPiou()
	{
        for (int i = 0; i < 3; i++)
        {
            GameObject t = Instantiate(tir as GameObject);
            PiouPiouScript p = t.AddComponent<PiouPiouScript>();
            p.direction = otherCaravane.transform.position + new Vector3(Random.value, Random.value, 0);
			var a = (otherCaravane.transform.position - transform.position).normalized;
            t.transform.position = transform.position + a * (i + 1);
        }
	}
}
