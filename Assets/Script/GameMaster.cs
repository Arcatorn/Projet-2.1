using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameMaster : MonoBehaviour
{

    public GameObject[] players;
    public PersoScript[] persoScripts;
    public int playerActif;
    public NavMeshAgent[] pc;
    Camera cam;
    public bool switching = false;
    float camSpeed = 50;
    public Vector3 offset;
    public static bool cursorIsOnCard = false;
    public static bool isPlayingACard = false;
    public static bool hittingAModule = false;
    public static int cardIDBeingPlayed = -1;
    public static bool endPlayingCard = false;
    public static GameObject moduleHit;
    public LineRenderer lr;
    public GameObject caj;
    public GameObject detector;
    CartesManager cartesManager;

    public Color[] colorPlayers;
    public Animator[] animButtons;

    void Awake()
    {
        playerActif = 0;
        pc[0].enabled = true;
        pc[1].enabled = true;
        cam = Camera.main;
        offset = new Vector3 (0,players[playerActif].transform.position.y - cam.transform.position.y, 0);
        pc[0].updateRotation = false;
        pc[1].updateRotation = false;
        cartesManager = GetComponent<CartesManager>();
        cam.backgroundColor = colorPlayers[0];
    }

    void Update()
    {
        if (isPlayingACard)
        {
            PlayerLine();
        }
        else if (endPlayingCard)
        {
            if (hittingAModule)
            {
                PlayCard();
            }
            hittingAModule = false;
            endPlayingCard = false;
        }
        else
        {
            if (lr.enabled)
            {
                lr.enabled = false;
                caj.SetActive(false);
                detector.SetActive(false);
            }
            

            if (!switching)
            {
                if (!cursorIsOnCard)
                {
                    ClicDetection();
                    Switch();
                    //Zoom();
                }
                //CameraFollow();
            }
            else
            {
                //MoveCamera();
            }
        }

       

    }

    void Switch()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switching = true;
            for (int i = 0; i < animButtons.Length; i++)
            {
                animButtons[i].SetTrigger("Switch");
                // faire un appel à carte Manager pour changer la main des joueurs
                playerActif = (playerActif + 1) % 2;
                switching = false;
                cam.backgroundColor = colorPlayers[playerActif];
            }
        }
    }


    void MoveCamera()
    {
        Vector3 camFictivPosition = new Vector3(cam.transform.position.x, players[playerActif].transform.position.y, cam.transform.position.z);
        Vector3 dir = (players[(playerActif + 1) % 2].transform.position - camFictivPosition).normalized;
        float dist = Vector3.Distance(players[(playerActif + 1) % 2].transform.position, camFictivPosition);
        if (dist > 2)
        {
            var distTot = Vector3.Distance(players[(playerActif + 1) % 2].transform.position, players[playerActif].transform.position);
            var distActual = Vector3.Distance(players[playerActif].transform.position, camFictivPosition);
            cam.backgroundColor = Color.Lerp(colorPlayers[playerActif], colorPlayers[(playerActif + 1) % 2], distActual / distTot);

            camFictivPosition += dir * Time.deltaTime * camSpeed;
            camFictivPosition.y = cam.transform.position.y;
            cam.transform.position = camFictivPosition;
        }
        else
        {
            playerActif = (playerActif + 1) % 2;
            switching = false;
            cam.transform.position = players[playerActif].transform.position - offset;
            cam.backgroundColor = colorPlayers[playerActif];
        }
    }

    void Zoom()
    {
        float a = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(a) > 0)
        {
            a *= 15;
            if ((cam.transform.position.y - a) >= 30 && (cam.transform.position.y - a) <= 90)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - a, cam.transform.position.z);
                offset = new Vector3(0, players[playerActif].transform.position.y - cam.transform.position.y, 0);
            }
        }
    }


    void CameraFollow()
    {
        if(pc[playerActif].velocity.magnitude > 0.05f)
        {
            cam.transform.position = players[playerActif].transform.position - offset;
        }
    }

    public void PlayerLine()
    {
        if (!lr.enabled)
        {
            lr.enabled = true;
            caj.SetActive(true);
            detector.SetActive(true);
        }
        var origin = players[playerActif].transform.position;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
		//point.z = -point.z + (Camera.main.transform.position.z * 2);
        //point.y = origin.y;
        

        RaycastHit hit;
        var dist = Vector3.Distance(origin, point);
        var dir = (point - Camera.main.transform.position);

        caj.transform.position = point + dir;
        int layer = ~(1<<11);
        if (Physics.Raycast(caj.transform.position, dir, out hit, dist, layer))
        {
            detector.transform.position = hit.point;
        }
        lr.SetPositions(new Vector3[] {Camera.main.transform.position, point + dir * 10});
    }

    public void PlayCard()
    {
         pc[playerActif].enabled = true;
        var c = moduleHit.transform.parent.gameObject.GetComponentInChildren(typeof (ConsoleScript)) as ConsoleScript;
        pc[playerActif].destination = c.pos;
        persoScripts[playerActif].carteID = cardIDBeingPlayed;
        persoScripts[playerActif].vaJouerUneCarte = true;
        cardIDBeingPlayed = -1;
    }


    public void ClicDetection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            RaycastHit hit;
            var dir = (point - Camera.main.transform.position);
            int layer = (1<<12);
            if (Physics.Raycast(point, dir, out hit, Mathf.Infinity, layer))
            {
                pc[playerActif].destination = hit.collider.gameObject.GetComponent<ConsoleScript>().pos;
            }
        }
    }

}
