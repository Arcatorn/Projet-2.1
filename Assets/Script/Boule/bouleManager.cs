using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum bouleEtat
{
    Iddle = 0,
    Bump = 1,
    Follow = 2,
    Acceleration = 3,
    Fantome = 4
}

public class bouleManager : MonoBehaviour
{
    [SerializeField] bouleEtat e = (bouleEtat)0;
    public Vector3 dirBumpToSend;
    public string[] names = {"None", "Bump", "Follow", "Acceleration", "Ghost"};
    public Sprite[] normal;
    public Sprite[] highlighted;
    public Image[] images;

    public void OnCollisionBoulePlayer(GameObject playerHitten)
    {
        if (e == bouleEtat.Bump)
        {
            playerHitten.GetComponent<playerBuffs>().buffActifs.bump = 1;
            playerHitten.GetComponent<playerBuffs>().buffActifs.bumpDir = dirBumpToSend;
            playerHitten.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Bump");
        }
        else if (e == bouleEtat.Follow)
        {
            playerHitten.GetComponent<playerBuffs>().buffActifs.follow += 3;
        }
        else if (e == bouleEtat.Acceleration)
        {
            playerHitten.GetComponent<playerBuffs>().buffActifs.acceleration += 3;
        }
        else if (e == bouleEtat.Fantome)
        {
            playerHitten.GetComponent<playerBuffs>().buffActifs.fantome += 3;
        }
    }

    public void SwitchBallEffects()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if ((int)e < 4){
                e = (bouleEtat)(e + 1);
            }
            else {
                e = (bouleEtat)0;
            }
            int a = (int)e;
            for (int i =0; i < images.Length; i++)
            {
                images[i].sprite = normal[i];
            }
            images[a].sprite = highlighted[a];
        }
    }
    void Update()
    {
       SwitchBallEffects(); 
    }
}
