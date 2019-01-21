using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SITA_3 : MonoBehaviour
{

    public int situationNumber = 1;

    private void Awake()
    {
        situationNumber = 1;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
    }

    private void Update()
    {
		NextVideo();
		SpecificVideo();
    }

    public void NextVideo()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (situationNumber < 4)
            {
                situationNumber += 1;
                GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
                MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
                a.Play();
            }
        }
    }

    public void SpecificVideo()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            situationNumber = 1;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            situationNumber = 2;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            situationNumber = 3;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            situationNumber = 4;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
    }

}
