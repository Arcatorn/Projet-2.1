using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("scenario1", LoadSceneMode.Single);
            GameObject.Find("Ragnaros").GetComponent<Flamepropagation>().StopAll();
            if (GameObject.Find("Plane (1)"))
            {
                GameObject.Find("Plane (1)").GetComponent<VideoSoundC>().StopAll();
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("scenario2", LoadSceneMode.Single);
            GameObject.Find("Ragnaros").GetComponent<Flamepropagation>().StopAll();
            if (GameObject.Find("Plane (1)"))
            {
                GameObject.Find("Plane (1)").GetComponent<VideoSoundC>().StopAll();
            }
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("scenario3", LoadSceneMode.Single);
            GameObject.Find("Ragnaros").GetComponent<Flamepropagation>().StopAll();
            if (GameObject.Find("Plane (1)"))
            {
                GameObject.Find("Plane (1)").GetComponent<VideoSoundC>().StopAll();
            }
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("TEST 1 Double NavMesh", LoadSceneMode.Single);
            GameObject.Find("Ragnaros").GetComponent<Flamepropagation>().StopAll();
            if (GameObject.Find("Plane (1)"))
            {
                GameObject.Find("Plane (1)").GetComponent<VideoSoundC>().StopAll();
            }
        }
    }
}
