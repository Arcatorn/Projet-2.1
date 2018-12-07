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
    float camSpeed = 60;
    public GameObject ps;
    GameObject activPS;

    void Awake()
    {
        playerActif = 0;
        pc[0].enabled = true;
        pc[1].enabled = true;
        cam = Camera.main;
        cam.transform.parent = players[playerActif].transform;
        pc[0].updateRotation = false;
        pc[1].updateRotation = false;
    }

    void Update()
    {
        if (!switching)
        {
            Switch();
            MovePlayerActif();
            Zoom();
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
            activPS = Instantiate(ps as GameObject);
            activPS.transform.position = players[playerActif].transform.position;
            cam.transform.parent = activPS.transform;
        }
    }

    void MovePlayerActif()
    {
        if (Input.GetMouseButton(0))
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
        Vector3 dir = (players[(playerActif + 1) % 2].transform.position - activPS.transform.position).normalized;
        float dist = Vector3.Distance(players[(playerActif + 1) % 2].transform.position, activPS.transform.position);
        if (dist > 2)
        {
            activPS.transform.position += dir * Time.deltaTime * camSpeed;
            Debug.Log(dist);
        }
        else
        {
            playerActif = (playerActif + 1) % 2;
            switching = false;
            cam.transform.parent = players[playerActif].transform;
            Destroy(activPS);
        }
    }

    void Zoom()
    {
        float a = Input.GetAxis("Mouse ScrollWheel");
        a *= 15;
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - a, cam.transform.position.z);
    }

}
