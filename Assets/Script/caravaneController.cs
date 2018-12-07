using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caravaneController : MonoBehaviour
{

    [SerializeField] float speed = 50;
    [SerializeField] float rotateSpeed = 5;
    Rigidbody rigid;
    Vector3 velocity = Vector3.zero;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(velocity.x) > 10)
			velocity.x = 10 * Mathf.Sign(velocity.x);
		if(Mathf.Abs(velocity.z )> 10)
			velocity.z = 10 * Mathf.Sign(velocity.z);
        rigid.velocity = velocity;
    }
    public void moveForward()
    {
        velocity -= transform.forward * speed * Time.deltaTime;
    }

    public void moveBackward()
    {
        velocity += transform.forward * speed * Time.deltaTime;
    }

    public void rotateRight()
    {
        var rotateVector = new Vector3(0, 30, 0);
        transform.Rotate(rotateVector * Time.deltaTime * rotateSpeed);
    }

    public void rotateLeft()
    {
        var rotateVector = new Vector3(0, -30, 0);
        transform.Rotate(rotateVector * Time.deltaTime * rotateSpeed);
    }

    public void fire()
    {

    }

    public void Stop()
    {
        velocity = Vector3.zero;
    }
}
