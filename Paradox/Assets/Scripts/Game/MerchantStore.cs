using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantStore : MonoBehaviour {

    public GameObject store,swordItem,appleItem;   
    private int appleMoney,swordMoney,appleNum,sword;
    public Text apple,appleCount;
    private float distance;
    public static int shopON;
    public static bool waves,storeAtive;
    public bool nb, eli, sal;
    public Transform npc;
    public GameObject[] easterEggs;
    private WaitForSeconds timeWait;
    
    void Start ()
    {
        ControlGame.mode = 1;
        appleNum = 0;
        waves = false;
        swordMoney = 50;
        appleMoney = 9;
        SetCountText();
    }	
	
	void Update ()
    {
        if (Input.GetKeyDown("r"))
        {
            if (appleNum > 0)
            {
                PlayerControl.hp += 20;
                appleNum -= 1;
            }
        }
        SetCountText();        
    }

    public void SetCountText()
    {
        apple.text = "" + appleMoney.ToString();
        appleCount.text = "x" + appleNum.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("e") && waves == true)
            {
                storeAtive = true;
                Debug.Log("store");
                StoreAtive();
            }
        }
    }

    public void StoreAtive()
    {
        Time.timeScale = 0;
        store.gameObject.SetActive(true);       
    }

    public void Apples()
    {
        if (ControlGame.money >= 9)
        {
            if (PlayerControl.hp < 100)
            {
                PlayerControl.hp += 20;
                ControlGame.money = ControlGame.money - appleMoney;
            }

            if (PlayerControl.hp >= 100)
            {
                PlayerControl.hp = 100;
                appleItem.gameObject.SetActive(true);
                appleNum += 1; 
                ControlGame.money = ControlGame.money - appleMoney;
            }
        }
   
        if (ControlGame.money <= 0)
        {
            ControlGame.money = 0;
        }
    }

    public void Sword()
    {
        if (ControlGame.money >= 50 && sword == 0)
        {
            sword = 1;
            swordItem.gameObject.SetActive(true);
            SwordmanAI.playerDamage += 50;
            SwordmanAI2.playerDamage += 50;
            ControlGame.money = ControlGame.money - swordMoney;
            StartCoroutine(DesativateSword());
        }
    }

    IEnumerator DesativateSword()
    {
        Debug.Log("ON");
        yield return new WaitForSeconds(60);
        Debug.Log("off");
        sword = 0;
        swordItem.gameObject.SetActive(false);
        SwordmanAI.playerDamage -= 50;
        SwordmanAI2.playerDamage -= 50;
    }

    public void StoreDesativate()
    {
        Time.timeScale = 1;
        storeAtive = false;
        store.gameObject.SetActive(false);
    }

    public void Niobio()
    {
        if (ControlGame.money >=0 && eli == false && sal == false)
        {
            nb = true;
            easterEggs[0].gameObject.SetActive(true);
        }
    }

    public void CloseNiobio()
    {
        nb = false;
        easterEggs[0].gameObject.SetActive(false);
    }

    public void Eli()
    {
        if (ControlGame.money >= 0 && nb == false && sal == false)
        {
            eli = true;
            easterEggs[1].gameObject.SetActive(true);
        }
    }

    public void CloseEli()
    {
        eli = false;
        easterEggs[1].gameObject.SetActive(false);
    }

    public void Salvynos()
    {
        if (ControlGame.money >= 0 && nb == false && eli == false)
        {
            sal = true;
            easterEggs[2].gameObject.SetActive(true);
        }
    }

    public void CloseSalvynos()
    {
        sal = false;
        easterEggs[2].gameObject.SetActive(false);
    }
}
