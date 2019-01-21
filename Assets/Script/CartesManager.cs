﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CartesTypes
{
    Yeux = 0,
    Estomac = 1,
    Griffes = 2,
    Pattes = 3,
    Bouche = 4,
    Truffe = 5
}

public enum SallesTypes
{
    Radar = 0,
    Electronics = 1,
    Navigation = 2,
    Defense = 3,
    Excavation = 4,
    Communication = 5
}

public class Cartes
{
    public int id;
    public CartesTypes cartesTypes;
    public GameObject cartePhysique;
    public Sprite illu;
    public Sprite picto;
    public int num;
    public Cartes(int _id, CartesTypes _cartesTypes, GameObject _cartePhysique)
    {
        id = _id;
        cartesTypes = _cartesTypes;
        cartePhysique = _cartePhysique;
        illu = Resources.Load<Sprite>("Sprites/Cartes/V4/Carte" + id.ToString());
        picto = Resources.Load<Sprite>("Sprites/Cartes/PictoNeon" + id.ToString());
        num = id + 1;
    }
}

public class Salle
{
    public SallesTypes sallesTypes;
    public GameObject salleGO;
    public SalleScript salleScript;
    public Salle(GameObject _salleGO, SallesTypes _sallesTypes)
    {
        salleGO = _salleGO;
        salleScript = _salleGO.GetComponent<SalleScript>();
        sallesTypes = _sallesTypes;
    }
}

public class CartesManager : MonoBehaviour
{

    public List<Cartes> allCards = new List<Cartes>();
    public List<Cartes> playerOneCards = new List<Cartes>();
    public List<Cartes> playerTwoCards = new List<Cartes>();
    public Image[] emplacements;
    CartesButtons[] cartesButtonsScripts = new CartesButtons[3];
    public CartesIndicators[] cartesIndicators;
    public GameObject[] cartesPhysiques = new GameObject[6];
    public List<Salle> allSalles = new List<Salle>();
    public static OnePlay toTransform;
    public static bool aPlayHasOccured = false;
    public GameMaster gameMaster;
    public AllActions allActions;
    public int nouveauPlayerActif = 0;

    public Color redCarteColor;
    public Color bleuCarteColor;
    public Color normalCarteColor;

    void Awake()
    {
        gameMaster = GetComponent<GameMaster>();
        allActions = GetComponent<AllActions>();
    }

    void Start()
    {
        Initialize();
    }


    void Update()
    {
        if (aPlayHasOccured)
        {
            TransformPlay();
        }
    }

    void Initialize()
    {
        for (int i = 0; i < 6; i++)
        {
            Cartes c = new Cartes(i, (CartesTypes)i, cartesPhysiques[i]);
            allCards.Add(c);
            if (i < 3)
            {
                cartesButtonsScripts[i] = emplacements[i].GetComponent<CartesButtons>();
                cartesButtonsScripts[i].id = i;
                emplacements[i].GetComponent<Image>().sprite = c.illu;
                playerOneCards.Add(c);
            }
            else
            {
                playerTwoCards.Add(c);
            }
        }
        SortCartes();
        InitializeSalles();
    }

    public struct OnePlay
    {
        public int cardID;
        public GameObject mGO;
        public int playerID;
    }

    public void PlayACardOnModule(int id, int _playerID)
    {
        OnePlay o = new OnePlay();
        o.cardID = id;
        o.mGO = gameMaster.persoScripts[_playerID].myConsole.gameObject.transform.parent.gameObject;
        o.mGO.transform.GetChild(0).GetComponent<ConsoleSound>().PlayCard();
        o.playerID = _playerID;
        toTransform = o;
        aPlayHasOccured = true;
    }

    void TransformPlay()
    {
        int idSalle = toTransform.mGO.GetComponent<SalleScript>().id;

        if (toTransform.playerID == 0)
        {
            allSalles[idSalle].salleScript.ChangeForPicto(allCards[toTransform.cardID].picto);
            allActions.CallAction(toTransform.cardID, idSalle, gameMaster.persoScripts[toTransform.playerID]);
            playerOneCards.Remove(allCards[toTransform.cardID]);
        }
        else
        {
            allSalles[idSalle].salleScript.ChangeForPicto(allCards[toTransform.cardID].picto);
            allActions.CallAction(toTransform.cardID, idSalle, gameMaster.persoScripts[toTransform.playerID]);
            playerTwoCards.Remove(allCards[toTransform.cardID]);
        }
        ChangerCarteNum(allCards[toTransform.cardID]);
        if (nouveauPlayerActif == toTransform.playerID)
        {
            SortCartes();
        }
        else
        {
            SortCartesIndicators();
        }

        aPlayHasOccured = false;
    }

    void InitializeSalles()
    {
        for (int i = 0; i < 5; i++)
        {

            Salle a = new Salle(GameObject.Find("Salle" + i.ToString()), (SallesTypes)i);
            allSalles.Add(a);
            a.salleScript.id = i;
            a.salleScript.myConsoleScript.RoomNumber = i;

        }
    }

    public void ChangerPictoMainDuJoueur()
    {
        if (nouveauPlayerActif == 0)
        {
            nouveauPlayerActif = 1;
        }
        else
        {
            nouveauPlayerActif = 0;
        }
        SortCartes();
    }

    void SortCartes()
    {
        if (nouveauPlayerActif == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Image m = cartesButtonsScripts[i].gameObject.GetComponent<Image>();
                if (i < playerOneCards.Count)
                {
                    m.sprite = playerOneCards[i].illu;
                    //cartesButtonsScripts[i].textMeshProComponent.SetText(ConvertisseurIntEnChiffreRomain(playerOneCards[i].num));
                    m.color = normalCarteColor;


                    if (gameMaster.persoScripts[0].vaJouerUneCarte)
                    {
                        if (playerOneCards[i].id == gameMaster.persoScripts[0].carteID)
                        {
                            cartesButtonsScripts[i].anim.SetBool("isPlayed", true);
                            m.color = new Color32(255, 255, 255, 20);
                        }
                    }
                    else
                    {
                        cartesButtonsScripts[i].anim.SetBool("isBlank", false);
                        cartesButtonsScripts[i].anim.SetBool("isPlayed", false);
                    }
                }
                else
                {
                    m.sprite = Resources.Load<Sprite>("Sprites/Cartes/CarteBlank");
                    cartesButtonsScripts[i].imageNumero.sprite = Resources.Load<Sprite>("Sprites/Cartes/CarteBlank");
                    cartesButtonsScripts[i].anim.SetBool("isPlayed", false);
                    cartesButtonsScripts[i].anim.SetBool("isBlank", true);
                    m.color = redCarteColor;
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Image m = cartesButtonsScripts[i].gameObject.GetComponent<Image>();
                if (i < playerTwoCards.Count)
                {
                    m.sprite = playerTwoCards[i].illu;
                    //cartesButtonsScripts[i].textMeshProComponent.SetText(ConvertisseurIntEnChiffreRomain(playerTwoCards[i].num));
                    m.color = normalCarteColor;


                    if (gameMaster.persoScripts[1].vaJouerUneCarte)
                    {
                        if (playerTwoCards[i].id == gameMaster.persoScripts[1].carteID)
                        {
                            cartesButtonsScripts[i].anim.SetBool("isPlayed", true);
                            m.color = new Color32(255, 255, 255, 20);
                        }
                    }
                    else
                    {
                        cartesButtonsScripts[i].anim.SetBool("isBlank", false);
                        cartesButtonsScripts[i].anim.SetBool("isPlayed", false);
                    }
                }
                else
                {
                    m.sprite = Resources.Load<Sprite>("Sprites/Cartes/CarteBlank");
                    cartesButtonsScripts[i].imageNumero.sprite = Resources.Load<Sprite>("Sprites/Cartes/CarteBlank");
                    cartesButtonsScripts[i].anim.SetBool("isPlayed", false);
                    cartesButtonsScripts[i].anim.SetBool("isBlank", true);
                    m.color = bleuCarteColor;
                }
            }
        }
        SortCartesIndicators();
    }

    public bool CheckHandisFull(int player)
    {
        bool toReturn = false;
        if (player == 0)
        {
            if (playerOneCards.Count == 3)
            {

                toReturn = true;
            }
        }
        else
        {
            if (playerTwoCards.Count == 3)
            {
                toReturn = true;
            }
        }

        return toReturn;
    }

    public void AjouterUneCarteDansLaMain(int player, int carteId)
    {
        if (player == 0)
        {
            playerOneCards.Add(allCards[carteId]);
            cartesPhysiques[carteId].transform.position = new Vector3(-100, 0, 0);
            CartePhysiqueScript s = cartesPhysiques[carteId].GetComponent<CartePhysiqueScript>();
            allSalles[s.salleID].salleScript.spotInUse[s.spotID] = false;
            allSalles[s.salleID].salleScript.spotCartes[s.spotID].GetComponentInChildren<Animator>().SetTrigger("Ferme");
        }
        else
        {
            playerTwoCards.Add(allCards[carteId]);
            cartesPhysiques[carteId].transform.position = new Vector3(-100, 0, 0);
            CartePhysiqueScript s = cartesPhysiques[carteId].GetComponent<CartePhysiqueScript>();
            allSalles[s.salleID].salleScript.spotInUse[s.spotID] = false;
            allSalles[s.salleID].salleScript.spotCartes[s.spotID].GetComponentInChildren<Animator>().SetTrigger("Ferme");
        }
        gameMaster.persoScripts[player].WantedCarteId = -1;
        if (nouveauPlayerActif == player)
        {
            SortCartes();
        }
        else
        {
            SortCartesIndicators();
        }

    }


    public void CancelOrderAnimBlank(int _cardID, int _player)
    {
        if (_player == 0)
        {
            for (int i = 0; i < playerOneCards.Count; i++)
            {
                if (playerOneCards[i].id == _cardID)
                {
                    cartesButtonsScripts[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 185);
                    cartesButtonsScripts[i].anim.SetBool("isPlayed", false);
                }
            }
        }
        else
        {
            for (int i = 0; i < playerTwoCards.Count; i++)
            {
                if (playerTwoCards[i].id == _cardID)
                {
                    cartesButtonsScripts[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 185);
                    cartesButtonsScripts[i].anim.SetBool("isPlayed", false);
                }
            }
        }
    }

    public void OrderAnimBlank(int _cardID, int _player)
    {
        if (_player == 0)
        {
            for (int i = 0; i < playerOneCards.Count; i++)
            {
                if (playerOneCards[i].id == _cardID)
                {
                    cartesButtonsScripts[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 20);
                    cartesButtonsScripts[i].anim.SetBool("isPlayed", true);
                }
            }
        }
        else
        {
            for (int i = 0; i < playerTwoCards.Count; i++)
            {
                if (playerTwoCards[i].id == _cardID)
                {
                    cartesButtonsScripts[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 20);
                    cartesButtonsScripts[i].anim.SetBool("isPlayed", true);
                }
            }
        }
    }

    Sprite ConvertisseurIntEnChiffreRomain(int toConvert)
    {
        Sprite toReturn = Resources.Load <Sprite> ("Sprites/Cartes/ChiffresRomains/Romain" + toConvert.ToString());
        return toReturn;
    }

    public void SortCartesIndicators()
    {
        int indicatorID = 0;
        for (int i = 0; i < allCards.Count; i++)
        {
            if (nouveauPlayerActif == 0)
            {
                if (!playerOneCards.Contains(allCards[i]))
                {
                    cartesIndicators[indicatorID].myImage.sprite = allCards[i].illu;
                    cartesIndicators[indicatorID].imageNumero.sprite = ConvertisseurIntEnChiffreRomain(allCards[i].num);
                    if (playerTwoCards.Contains(allCards[i]))
                    {
                        if (gameMaster.persoScripts[1].carteID == i)
                        {
                            cartesIndicators[indicatorID].ImAutrePersoVaLaJouer();
                        }
                        else
                        {
                            cartesIndicators[indicatorID].ImAutrePerso();
                        }
                    }
                    else
                    {
                        if (cartesPhysiques[i].GetComponent<CartePhysiqueScript>().canBeInteract)
                        {
                            if (playerOneCards.Count < 3)
                            {
                                if (gameMaster.persoScripts[0].WantedCarteId == i)
                                {
                                    cartesIndicators[indicatorID].ImVaLaChercher();
                                }
                                else
                                {
                                    cartesIndicators[indicatorID].ImRassable();
                                }
                            }
                            else
                            {
                                cartesIndicators[indicatorID].ImRassableFull();
                            }
                            if (gameMaster.persoScripts[1].WantedCarteId == i)
                            {
                                cartesIndicators[indicatorID].ImAutrePersoVaLaChercher();
                            }
                        }
                        else
                        {
                            cartesIndicators[indicatorID].ImUsedByConsole();
                        }
                    }
                    indicatorID++;
                }
                else
                {
                    int a = playerOneCards.IndexOf(allCards[i]);
                    cartesButtonsScripts[a].imageNumero.sprite = ConvertisseurIntEnChiffreRomain(allCards[i].num);
                }
            }
            else
            {
                if (!playerTwoCards.Contains(allCards[i]))
                {
                    cartesIndicators[indicatorID].myImage.sprite = allCards[i].illu;
                    cartesIndicators[indicatorID].imageNumero.sprite = ConvertisseurIntEnChiffreRomain(allCards[i].num);
                    if (playerOneCards.Contains(allCards[i]))
                    {
                        if (gameMaster.persoScripts[0].carteID == i)
                        {
                            cartesIndicators[indicatorID].ImAutrePersoVaLaJouer();
                        }
                        else
                        {
                            cartesIndicators[indicatorID].ImAutrePerso();
                        }
                    }
                    else
                    {
                        if (cartesPhysiques[i].GetComponent<CartePhysiqueScript>().canBeInteract)
                        {
                            if (playerTwoCards.Count < 3)
                            {
                                if (gameMaster.persoScripts[1].WantedCarteId == i)
                                {
                                    cartesIndicators[indicatorID].ImVaLaChercher();
                                }
                                else
                                {
                                    cartesIndicators[indicatorID].ImRassable();
                                }
                            }
                            else
                            {
                                cartesIndicators[indicatorID].ImRassableFull();
                            }
                            if (gameMaster.persoScripts[0].WantedCarteId == i)
                            {
                                cartesIndicators[indicatorID].ImAutrePersoVaLaChercher();
                            }
                        }
                        else
                        {
                            cartesIndicators[indicatorID].ImUsedByConsole();
                        }
                    }
                    indicatorID++;
                }
                else{
                    int a = playerTwoCards.IndexOf(allCards[i]);
                    cartesButtonsScripts[a].imageNumero.sprite = ConvertisseurIntEnChiffreRomain(allCards[i].num);
                }
            }
        }

        if (indicatorID <= 5)
        {
            int remainingIndicators = 6 - indicatorID;
            for (int i = 0; i < remainingIndicators; i++)
            {
                cartesIndicators[i + indicatorID].ImDisabled();
            }
        }

    }

    public void ChangerCarteNum(Cartes cardPlayed)
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            if (allCards[i].num > cardPlayed.num)
            {
                allCards[i].num -= 1;
            }
        }
        cardPlayed.num = 6;
    }

    public void BaisserLesCartes()
    {
        for (int i = 0; i < cartesButtonsScripts.Length; i++)
        {
            cartesButtonsScripts[i].anim.SetBool("Baisser", true);
        }
    }

    public void ReleverLesCartes()
    {
        for (int i = 0; i < cartesButtonsScripts.Length; i++)
        {
            cartesButtonsScripts[i].anim.SetBool("Baisser", false);
        }
    }

}

