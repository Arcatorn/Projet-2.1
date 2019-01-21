using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject caj;
    CartesManager cartesManager;

    private CardSound cardSound;
    public Animator[] animButtons;
    public Animator mainDuJoueur;
    public static bool actionSpeciale = false;
    public GameObject boutonSpecialeAction;
    public GameObject boutonSpecialeCancel;
    public GameObject[] flecheEtNum;

    void Awake()
    {
        cardSound = Camera.main.GetComponent<CardSound>();
        playerActif = 0;
        pc[0].enabled = true;
        pc[1].enabled = true;
        cam = Camera.main;
        offset = new Vector3(0, players[playerActif].transform.position.y - cam.transform.position.y, 0);
        pc[0].updateRotation = false;
        pc[1].updateRotation = false;
        cartesManager = GetComponent<CartesManager>();
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
            if (caj.activeInHierarchy)
            {
                caj.SetActive(false);
            }


            if (!switching)
            {
                if (!cursorIsOnCard && !actionSpeciale)
                {
                    ClicDetection();
                    Switch();
                }
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
                persoScripts[playerActif].lumierePerso.SetActive(false);
                playerActif = (playerActif + 1) % 2;
                persoScripts[playerActif].lumierePerso.SetActive(true);
                switching = false;
                cartesManager.ChangerPictoMainDuJoueur();
                Cursor.SetCursor(Resources.Load<Texture2D>("Sprites/Cursor/Cursor" + playerActif) as Texture2D, Vector2.zero, CursorMode.Auto);
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
            //cam.backgroundColor = Color.Lerp(colorPlayers[playerActif], colorPlayers[(playerActif + 1) % 2], distActual / distTot);

            camFictivPosition += dir * Time.deltaTime * camSpeed;
            camFictivPosition.y = cam.transform.position.y;
            cam.transform.position = camFictivPosition;
        }
        else
        {
            playerActif = (playerActif + 1) % 2;
            switching = false;
            cam.transform.position = players[playerActif].transform.position - offset;
            //cam.backgroundColor = colorPlayers[playerActif];
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
        if (pc[playerActif].velocity.magnitude > 0.05f)
        {
            cam.transform.position = players[playerActif].transform.position - offset;
        }
    }

    public void PlayerLine()
    {
        if (!caj.activeInHierarchy)
        {
            caj.SetActive(true);
        }
        var origin = players[playerActif].transform.position;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));


        RaycastHit hit;
        var dist = Vector3.Distance(origin, point);
        var dir = (point - Camera.main.transform.position);

        caj.transform.position = point + dir;
        int layer = (1 << 10);
        if (Physics.Raycast(caj.transform.position, dir, out hit, dist, layer))
        {
            SalleScript salleScript = hit.collider.gameObject.transform.parent.gameObject.GetComponent<SalleScript>();
            if (salleScript.myConsoleScript.persoOnMeID == playerActif || salleScript.myConsoleScript.persoOnMe == false)
            {
                if (salleScript.textConsole.enabled == true && !salleScript.isFull && persoScripts[playerActif].canReceiveOrder)
                {
                    caj.GetComponent<Animator>().SetBool("Dance", true);
                    salleScript.OnDragCardOnMe();
                    hittingAModule = true;
                    moduleHit = hit.transform.gameObject;
                }
            }
        }
        else
        {
            if (moduleHit != null)
            {
                moduleHit.transform.parent.gameObject.GetComponent<SalleScript>().OnExitCardOnMe();
                moduleHit = null;
                hittingAModule = false;
                caj.GetComponent<Animator>().SetBool("Dance", false);
            }
        }
    }

    public void PlayCard()
    {
        pc[playerActif].enabled = true;
        var c = moduleHit.transform.parent.gameObject.GetComponentInChildren(typeof(ConsoleScript)) as ConsoleScript;
        c.gameObject.transform.parent.gameObject.GetComponent<SalleScript>().OnExitCardOnMe();
        pc[playerActif].destination = c.pos;
        BeforeCancelOrder();
        persoScripts[playerActif].CancelOrder();
        persoScripts[playerActif].OrderGoPlayACard(cardIDBeingPlayed, c);
        cartesManager.OrderAnimBlank(cardIDBeingPlayed, playerActif);
        cardIDBeingPlayed = -1;
        cardSound.GoingToPlayACard();

    }


    public void ClicDetection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            RaycastHit hit;
            var dir = (point - Camera.main.transform.position);
            int layer = (1 << 12);
            if (Physics.Raycast(point, dir, out hit, Mathf.Infinity, layer))
            {
                ConsoleScript consoleScript = hit.collider.gameObject.GetComponent<ConsoleScript>();
                if (consoleScript.persoOnMe == false && persoScripts[playerActif].canReceiveOrder)
                {
                    pc[playerActif].destination = consoleScript.pos;
                    BeforeCancelOrder();
                    persoScripts[playerActif].CancelOrder();
                    persoScripts[playerActif].OrderGoToConsole(consoleScript);
                }
            }

            int layer2 = (1 << 13);
            if (Physics.Raycast(point, dir, out hit, Mathf.Infinity, layer2))
            {
                if (!cartesManager.CheckHandisFull(playerActif) && persoScripts[playerActif].canReceiveOrder)
                {
                    if (hit.collider.gameObject.GetComponent<CartePhysiqueScript>().canBeInteract)
                    {
                        int _wantedCardID = hit.collider.gameObject.GetComponent<CartePhysiqueScript>().id;
                        pc[playerActif].destination = hit.collider.gameObject.transform.position;
                        BeforeCancelOrder();
                        persoScripts[playerActif].CancelOrder();
                        persoScripts[playerActif].OrderGoGetACard(_wantedCardID);
                        cartesManager.SortCartesIndicators();
                    }
                }
            }
        }
    }

    public void BeforeCancelOrder()
    {
        if (persoScripts[playerActif].vaJouerUneCarte)
        {
            cartesManager.CancelOrderAnimBlank(persoScripts[playerActif].carteID, playerActif);
        }
    }

    public void ButtonClickSpecialAction()
    {
        if (actionSpeciale)
        {
            cartesManager.ReleverLesCartes();
            for (int i = 0; i < flecheEtNum.Length; i++)
            {
                flecheEtNum[i].SetActive(false);
            }
            boutonSpecialeAction.SetActive(true);
            boutonSpecialeCancel.SetActive(false);
            GameObject.Find("Bouton").GetComponent<Bouton>().CancelBouton();
            actionSpeciale = false;
        }
        else if (!actionSpeciale)
        {
            cartesManager.BaisserLesCartes();
            SetNumerosBoutons();
            boutonSpecialeAction.SetActive(false);
            boutonSpecialeCancel.SetActive(true);
            actionSpeciale = true;
            GameObject.Find("Bouton").GetComponent<Bouton>().ClicBouton();
        }
    }

    public void EnabledBoutonSpecialeAction(bool a)
    {
        boutonSpecialeAction.SetActive(a);
    }

    public void SetNumerosBoutons()
    {
        for (int i = 0; i < flecheEtNum.Length; i++)
        {
            if (playerActif == 0)
            {
                if (i < cartesManager.playerOneCards.Count)
                {
                    int num = cartesManager.playerOneCards[i].num;
                    flecheEtNum[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Cartes/Numeros/Numero" + num);
                    flecheEtNum[i].SetActive(true);
                }
            }
            else
            {
                if (i < cartesManager.playerTwoCards.Count)
                {
                    int num = cartesManager.playerTwoCards[i].num;
                    flecheEtNum[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Cartes/Numeros/Numero" + num);
                    flecheEtNum[i].SetActive(true);
                }
            }
        }
    }

}
