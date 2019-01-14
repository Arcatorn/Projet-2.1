using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameMaster : MonoBehaviour
{

    public GameObject[] players;
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
            }
        }

        if (!switching)
        {
            if (!cursorIsOnCard)
            {
                Switch();
                //MovePlayerActif();
                //Zoom();
            }
            CameraFollow();
        }
        else
        {
            MoveCamera();
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
            }
        }
    }

    void MovePlayerActif()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                pc[playerActif].destination = hit.point;
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
        }
        var origin = players[playerActif].transform.position;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Camera.main.pixelHeight - Input.mousePosition.y, Camera.main.nearClipPlane));
		point.z = -point.z + (Camera.main.transform.position.z * 2);
        point.y = origin.y;
        caj.transform.position = point;

        RaycastHit hit;
        var dist = Vector3.Distance(origin, point);
        var dir = (point - origin).normalized;
        if (Physics.Raycast(origin, dir, out hit, dist))
        {
            point = hit.point;
            // if (hit.transform.tag == "Module")
            // {
            //     hittingAModule = true;
            //     moduleHit = hit.transform.gameObject;
            // }
            // else
            // {
            //     hittingAModule = false;
            // }
        }
        lr.SetPositions(new Vector3[] {origin, point});
    }

    public void PlayCard()
    {
        cartesManager.PlayACardOnModule(cardIDBeingPlayed);
        cardIDBeingPlayed = -1;
    }

}
