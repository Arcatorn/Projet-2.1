using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformManager : MonoBehaviour 
{
	[SerializeField] GameObject[] platform;

	void Awake()
	{
		disableAllPlatform();
	}

	IEnumerator PlatformDesactivation()
	{
		platform[0].SetActive(true);
		yield return new WaitForSeconds(1);
		platform[0].SetActive(false);
		platform[1].SetActive(true);
		yield return new WaitForSeconds(1);
		platform[1].SetActive(false);
		platform[2].SetActive(true);
		yield return new WaitForSeconds(1);
		platform[2].SetActive(false);
		platform[3].SetActive(true);
		yield return new WaitForSeconds(1);
		disableAllPlatform();
	}

	void disableAllPlatform()
	{
		foreach(GameObject go in platform)
		{
			go.SetActive(false);
		}
		StartCoroutine("PlatformDesactivation");
	}
}
