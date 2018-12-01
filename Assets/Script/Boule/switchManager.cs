using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Rendering.PostProcessing;

public class switchManager : MonoBehaviour
{
    public bool whileOnPlayer = false;
    public bool playerOnBall = false;
    public bool taperdanslaballe = false;
    public GameObject actualPlayer;
    private int actualPlayerIndex = 0;

    public GameObject touchedByBallObject;
    GameObject cercleOfActualPlayer;

    private PingPong ping;
    public PostProcessVolume ppv;
    public PostProcessProfile ppp_1;
    public PostProcessProfile ppp_2;

    public GameObject[] cercles;

    private void Awake()
    {
        ping = GetComponent<PingPong>();
        selectPlayer(actualPlayerIndex);
        updateCameraCall(actualPlayer);
    }

    private void Update()
    {
        
        if (whileOnPlayer)
        {
            cercleOfActualPlayer = actualPlayer.transform.GetChild(3).gameObject;
            switchTest(); 
            if(!cercleOfActualPlayer.activeInHierarchy)
                cercleOfActualPlayer.SetActive(true);
        }
        else{
            cercles[0].SetActive(false);
            cercles[1].SetActive(false);
        }
    }

    void switchTest()
    {
        if (actualPlayer == touchedByBallObject && Input.GetKeyDown(KeyCode.Space))
        {
            ping.setTimerAtMax();
            DisablePlayer();
            actualPlayerIndex = (actualPlayerIndex + 1) % 2;
            selectPlayer(actualPlayerIndex);
            playerOnBall = true;
            updateCameraCall(ping.boule);
            if (actualPlayerIndex == 0)
            {
               ppv.profile = ppp_1;
            }
            else{
                ppv.profile = ppp_2;
            }
            FMODUnity.RuntimeManager.PlayOneShot("event:/Switch");
        }
    }

    void updateCameraCall(GameObject wanted)
    {
        Camera.main.GetComponent<cameraFollow>().selectObjectToLook(wanted);
    }

    public void playerOnBallTouchedPlayer()
    {
        playerOnBall = false;
        ActivatePlayer();
        updateCameraCall(actualPlayer);
    }

    void DisablePlayer()
    {
        actualPlayer.GetComponent<playerMovement>().disableForceField();
        actualPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;

         if(actualPlayer.transform.GetChild(3).gameObject.activeInHierarchy)
            actualPlayer.transform.GetChild(3).gameObject.SetActive(false);
    }

    void ActivatePlayer()
    {
        //actualPlayer.GetComponent<playerMovement>().enabled = true;
    }

    public void selectPlayer(int wantedPlayer)
    {
        actualPlayer = ping.players[wantedPlayer];
    }

    void TaperDansLaBaballe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            taperdanslaballe = true;
            ping.playerCible = (ping.playerCible + 1) % 2;
        }
    }
}
