using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public enum etatPingPong
{
    launch = 0,
    voyage = 1,
    Collision = 2,
    onPlayer = 3
}


public class PingPong : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] etatPingPong e = (etatPingPong)0;
    public GameObject boule;
    public GameObject[] players = new GameObject[2];
    public int playerActif = 0;
    public int playerCible;

    public float bouleVitesse = 10;
    public float bouleVitesseIni;
    public float timer = 0.0f;
    public float timerMax = 1.0f;
    switchManager switchScript;
    bouleManager bouleManager;

    Vector3 dirBouleCollision;

    void Awake()
    {
        bouleVitesseIni = bouleVitesse;
        boule = GameObject.Find("Sphere");
        Vector3 dir = (players[(playerActif + 1) % 2].transform.position - players[playerActif].transform.position).normalized;
        boule.transform.position = players[playerActif].transform.position + dir * 2;
        switchScript = GetComponent<switchManager>();
        bouleManager = boule.GetComponent<bouleManager>();
    }

    void FixedUpdate()
    {
        switch (e)
        {
            case etatPingPong.launch:
                Launch();
                e = etatPingPong.voyage;
                break;
            //
            case etatPingPong.voyage:
                Travel();
                break;
            //
            case etatPingPong.Collision:
                Collision();
                e = etatPingPong.onPlayer;
                boule.gameObject.SetActive(false);
                break;
            //
            case etatPingPong.onPlayer:
                inputTimer();
                break;
        }
    }

    void Launch()
    {
        playerCible = (playerActif + 1) % 2;
        boule.transform.SetParent(null);
    }

    void Travel()
    {
        var directionWanted = (players[playerCible].transform.position - boule.transform.position).normalized;
        var velocityVector = Vector3.zero;
        if (switchScript.playerOnBall || switchScript.taperdanslaballe)
        {
            velocityVector = directionWanted * bouleVitesseIni * 2 * Time.deltaTime;
        }
        else
        {
            velocityVector = directionWanted * bouleVitesseIni * Time.deltaTime;
        }
        if (testDetection(velocityVector))
        {
            e = etatPingPong.Collision;
            bouleVitesse = bouleVitesseIni;
            boule.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            boule.GetComponent<Rigidbody>().velocity = velocityVector;
        }
    }
    bool testDetection(Vector3 position)
    {
        bool detection = false;
        RaycastHit hit;
        var magnitude = position.magnitude * Time.deltaTime;
        if (Physics.Raycast(boule.transform.position, position, out hit, magnitude * 2, layer))
        {
           
            if (hit.collider.name == "Champ de force")
            {
                playerActif = playerCible;
                e = etatPingPong.launch;
            }
            else if (hit.collider.gameObject == players[playerCible])
            {
                detection = true;
                dirBouleCollision = -(players[playerCible].transform.position - boule.transform.position).normalized;
            }
        }
        return detection;
    }

    void Collision()
    {
        boule.GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (switchScript.actualPlayer != players[playerCible])
        {
            timer = timerMax;
        }
        playerActif = playerCible;
        if (switchScript.playerOnBall)
        {
            switchScript.playerOnBallTouchedPlayer();
        }
        else
        {
            bouleManager.OnCollisionBoulePlayer(players[playerActif]);
            bouleManager.dirBumpToSend = dirBouleCollision;
            if (players[playerCible] != switchScript.actualPlayer)
            FMODUnity.RuntimeManager.PlayOneShot("event:/FootStep");
        }
    }


    void inputTimer()
    {
        if (timer == 0.0f)
        {
            switchScript.taperdanslaballe = false;
            boule.transform.SetParent(players[playerActif].transform);
            switchScript.whileOnPlayer = true;
            switchScript.touchedByBallObject = players[playerActif];
        }
        Vector3 dir = (players[(playerActif + 1) % 2].transform.position - players[playerActif].transform.position).normalized;
        boule.transform.position = players[playerActif].transform.position + dir * 2;
        timer += Time.deltaTime;

        if (timer >= timerMax)
        {
            switchScript.whileOnPlayer = false;
            e = etatPingPong.launch;
            boule.gameObject.SetActive(true);
            timer = 0.0f;
        }
    }
    public void setTimerAtMax()
    {
        timer = timerMax;
    }
}
