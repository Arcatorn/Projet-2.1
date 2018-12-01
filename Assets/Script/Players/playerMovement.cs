using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public GameObject animator;
    Animator anim;
    Rigidbody rigid;
    public float speed;
    public float baseSpeed = 300;
    Collider childrenCollider;
    GameObject boule;
    GameObject bouclier;
    playerBuffs playerBuffs;
    switchManager switchManager;

    void Awake()
    {
    	anim = animator.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        childrenCollider = transform.GetChild(0).GetComponent<Collider>();
        boule = GameObject.Find("Sphere");
        bouclier = transform.GetChild(1).gameObject;
        playerBuffs = GetComponent<playerBuffs>();
        switchManager = GameObject.Find("Master").GetComponent<switchManager>();
    }

    void FixedUpdate()
    {

    	if (switchManager.actualPlayer == gameObject && playerBuffs.buffActifs.bump == 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (rigid.velocity.magnitude > 0)
                {
                    rigid.velocity = Vector3.zero;
                }

                if ((boule.transform.position - transform.position).magnitude > childrenCollider.transform.localScale.x / 2)
                {
                    forceField();
                    anim.SetBool("Running", false);
                	anim.SetBool("Shield", true);
                }
            }
            else
            {
                Move();
                disableForceField();
            }

        }
        else
        {
        	anim.SetBool("Running", false);
        }
        if (playerBuffs.buffActifs.follow > 0 && playerBuffs.buffActifs.bump == 0)
        {
            Move();
        }
    
  }
    void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");

        if(Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
        	anim.SetBool("Running", true);
        }
        else
        {
        	anim.SetBool("Running", false);
        }
		anim.SetFloat("SpeedX",x);
		anim.SetFloat("SpeedZ",z);
        var directionVector = new Vector3(x, 0, z);
        directionVector = directionVector.normalized;
        Vector3 newDir = Vector3.RotateTowards(animator.transform.forward, directionVector, Time.deltaTime * 5, 0.0f);
        animator.transform.rotation = Quaternion.LookRotation(newDir);

        var velocityy = directionVector * Time.deltaTime * speed;
        rigid.velocity = velocityy;
    }

    void forceField()
    {
        if (!childrenCollider.enabled)
            childrenCollider.enabled = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Travel_Loop");
        if (!bouclier.activeInHierarchy)
            bouclier.SetActive(true);
    }

    public void disableForceField()
    {
    	anim.SetBool("Shield", false);

        if (childrenCollider.enabled)
            childrenCollider.enabled = false;

        if (bouclier.activeInHierarchy)
            bouclier.SetActive(false);
    }
}


