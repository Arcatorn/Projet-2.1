using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

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

    private void Awake()
    {
        ping = GetComponent<PingPong>();
        selectPlayer(actualPlayerIndex);
        updateCameraCall(actualPlayer);
    }

    private void Update()
    {
        cercleOfActualPlayer = actualPlayer.transform.GetChild(3).gameObject;
        if (whileOnPlayer)
        {
            switchTest(); 
            if(!cercleOfActualPlayer.activeInHierarchy)
                cercleOfActualPlayer.SetActive(true);
        }
        else{
             if(cercleOfActualPlayer.activeInHierarchy)
                cercleOfActualPlayer.SetActive(false);
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
        //actualPlayer.GetComponent<playerMovement>().enabled = false;
        actualPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
