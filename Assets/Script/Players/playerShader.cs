using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShader : MonoBehaviour {

	public Material[] m;
	public Material material_joint;
	public Material material_surface;
	public int index = 0;
	float dissolutionTime = -1;
	public GameObject joint;
	public GameObject surface;

	private void Start() 
	{
		//material_joint = joint.GetComponent<Material>();
		//material_surface = surface.GetComponent<Material>();
	}

	void Update() 
	{
		if (index == 1)
		{
			Dissolution();
		}
	}

	public void PutBaseSkin()
	{
		joint.GetComponent<SkinnedMeshRenderer>().material = m[0];
		surface.GetComponent<SkinnedMeshRenderer>().material = m[0];
		index = 0;
	}
	
	public void PutDissolve()
	{
		joint.GetComponent<SkinnedMeshRenderer>().material = m[1];
		surface.GetComponent<SkinnedMeshRenderer>().material = m[1];
		index = 1;
	}

	public void PutGhost()
	{
		joint.GetComponent<SkinnedMeshRenderer>().material = m[2];
		surface.GetComponent<SkinnedMeshRenderer>().material = m[2];
		index = 2;
	}

	void Dissolution()
	{
		dissolutionTime = Mathf.Clamp(dissolutionTime + Time.deltaTime, -1, 1);
		material_joint.SetFloat("Vector1_C45ECC06", dissolutionTime);
		material_surface.SetFloat("Vector1_C45ECC06", dissolutionTime);
		if (dissolutionTime == 1)
		{
			PutGhost();
			dissolutionTime = 0;
		}
	}

}
