using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caravaneController : MonoBehaviour
{
    [SerializeField] GameObject caravaneToMove;
	[SerializeField] GameObject[] buttons;
	
	void FixedUpdate()
	{
		caravaneToMove.transform.Translate(caravaneToMove.transform.forward * 5 * Time.deltaTime);
	}

	public void detectButton(GameObject wantedGameObject)
	{
		GameObject go;
		for(int i = 0; i < buttons.Length;i++)
		{
			if(wantedGameObject == buttons[i])
			{
				go = buttons[i];
				print(go.name);
			}
		}
	}
}
