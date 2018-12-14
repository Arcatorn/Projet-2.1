using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour {

	public GameObject cible;
	public static bool mustShoot = false;
	public GameObject projectile;

	float timer = 0;
	float timerMax = 2.5f;
	void Start () {
		
	}

    void Update()
    {
        if (cible)
        {
            transform.LookAt(cible.transform.position);
        }
        else
        {
            var l = LayerMask.GetMask("Ennemis");
            Collider[] c = Physics.OverlapSphere(transform.position, 50, l);
            if (c.Length > 0)
            {
                cible = c[0].gameObject;
            }
        }

        if (mustShoot)
        {
            // timer += Time.deltaTime;
            // if (timer >= timerMax)
            // {
            // 	Shoot();
            // 	timer = 0;
            // }
            Shoot();
        }
    }

    void Shoot()
	{
		GameObject p = Instantiate(projectile as GameObject);
		p.transform.position = transform.position + transform.forward * 5;
		p.GetComponent<Projectile>().direction = transform.forward;
		mustShoot = false;
	}
}
