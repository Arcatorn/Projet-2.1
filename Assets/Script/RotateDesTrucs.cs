using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDesTrucs : MonoBehaviour {
    public float RotateSpeed;

	void Update ()
    {
        transform.Rotate(0, RotateSpeed, 0);
    }
}
