using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartePhysiqueScript : MonoBehaviour {

	public int id;
	public int salleID;
	public int spotID;
	public bool canBeInteract = false;
	CartesManager cartesManager;

	private void Awake() {
		cartesManager = GameObject.Find("GameMaster").GetComponent<CartesManager>();
	}

    public IEnumerator MonterLeCube()
    {
        while (true)
        {
			Vector3 pos = transform.position;
			pos.y += 0.05f;
			transform.position = pos;
            if (transform.position.y >= -1.5f)
            {
				
				canBeInteract = true;
				cartesManager.SortCartesIndicators();
                yield break;
            }
            else
            {
                yield return null;
            }
        }

    }
}
