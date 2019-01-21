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
                StartCoroutine("SuperCoroutine" + situationNumber.ToString());
            }
        }
    }

    public void SpecificVideo()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine("SuperCoroutine1");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            StartCoroutine("SuperCoroutine2");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            StartCoroutine("SuperCoroutine3");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            StartCoroutine("SuperCoroutine4");
        }
    }

    IEnumerator SuperCoroutine1()
    {
        situationNumber = 1;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine2()
    {
        situationNumber = 2;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine3()
    {
        situationNumber = 3;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine4()
    {
        situationNumber = 4;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

}
