using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SITA_1 : MonoBehaviour
{

    public int situationNumber = 1;
	VideoSoundsA soundsA;
    private void Awake()
    {
		soundsA = GetComponent<VideoSoundsA>();
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
            if (situationNumber < 10)
            {
                situationNumber += 1;
                GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
                MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
                a.Play();
            }
        }
    }

    public void SpecificVideo()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            situationNumber = 10;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
		   situationNumber = 1;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            situationNumber = 2;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
			soundsA.StopAll();
			soundsA.SituationUne();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            situationNumber = 3;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            situationNumber = 4;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            situationNumber = 5;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            situationNumber = 6;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            situationNumber = 7;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
		 else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            situationNumber = 8;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }
		 else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            situationNumber = 9;
            GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/A/SITA_" + situationNumber.ToString()) as Material;
            MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            a.Play();
        }

    }

}
