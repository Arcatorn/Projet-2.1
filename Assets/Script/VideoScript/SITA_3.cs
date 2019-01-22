﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SITA_3 : MonoBehaviour
{

    public int situationNumber = 1;
    VideoSoundC soundC;
    AllActions allActions;
    private void Awake() {
        allActions = GameObject.Find("GameMaster").GetComponent<AllActions>();
    }

    private void Start()
    {
        soundC = GetComponent<VideoSoundC>();
        situationNumber = 1;
        StartCoroutine("SuperCoroutine1");
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
            if (situationNumber < 3)
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
    }

    IEnumerator SuperCoroutine1()
    {
        situationNumber = 1;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Stop();
        a.Play();
        soundC.StopAll();
        soundC.SituationUne();
        yield break;
    }

    IEnumerator SuperCoroutine2()
    {
        situationNumber = 2;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Stop();
        a.Play();
        yield break;
    }

    IEnumerator SuperCoroutine3()
    {
        situationNumber = 3;
        GetComponent<Renderer>().material = Resources.Load<Material>("VIDEOS/C/SITC_" + situationNumber.ToString()) as Material;
        MovieTexture a = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        a.Stop();
        a.Play();
        soundC.StopAll();
        soundC.SituationDeux();
        yield return new WaitForSeconds(31);
        GameObject.Find("GameMaster").GetComponent<VehiculeFB>().EtatTempete();
        yield break;
    }

}
