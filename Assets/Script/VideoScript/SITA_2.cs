using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SITA_2 : MonoBehaviour
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
            if (situationNumber < 9)
            {
                situationNumber += 1;
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
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            StartCoroutine("SuperCoroutine5");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            StartCoroutine("SuperCoroutine6");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            StartCoroutine("SuperCoroutine7");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            StartCoroutine("SuperCoroutine8");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            StartCoroutine("SuperCoroutine9");
        }
    }

    IEnumerator SuperCoroutine1()
    {
        situationNumber = 1;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine2()
    {
        situationNumber = 2;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine3()
    {
        situationNumber = 3;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine4()
    {
        situationNumber = 4;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine5()
    {
        situationNumber = 5;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine6()
    {
        situationNumber = 6;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine7()
    {
        situationNumber = 7;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine8()
    {
        situationNumber = 8;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine9()
    {
        situationNumber = 9;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/B/SITB_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Play();
        yield break;
    }

}
