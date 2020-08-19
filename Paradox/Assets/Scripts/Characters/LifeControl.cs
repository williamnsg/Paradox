using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeControl : MonoBehaviour
{
    public Slider lifeBar;
    public Text percent;

    public void Start()
    {
        SetPercentText();
    }

    public void Update()
    {
        SetPercentText();
        lifeBar.value = PlayerControl.hp;        
    }

    public void SetPercentText()
    {
        percent.text = "" + PlayerControl.hp.ToString() + "%";

        if (PlayerControl.hp <= 0)
        {
            PlayerControl.hp = 0;
        }
    }
}