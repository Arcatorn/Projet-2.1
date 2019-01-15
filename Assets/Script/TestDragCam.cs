using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDragCam : MonoBehaviour {

bool pause = false;

	void Start () {
		
	}
	
	
	void Update () {
		monTest();
	}

	public void monTest()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			if(!pause)
			{
				Time.timeScale = 0;
				pause = true;
			}
			else{
				Time.timeScale = 1;
				pause = false;
			}
		}
	}
}
