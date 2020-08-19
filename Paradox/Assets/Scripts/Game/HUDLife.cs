using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLife : MonoBehaviour
{
    public GameObject[] hearts;
    public GameObject[] special;
    public GameObject[] faces;
	public static int deathCountHUD;
    public static int pontuacao;
    public Text pontos;


    void Start()
    {
		deathCountHUD = 0;
        pontuacao = 0;
        Faces();
    }

     void Update()
    {
        Points();
    }
    public void Points()
    {
        pontos.text = pontuacao.ToString();
        PlayerPrefs.SetInt("pontuacao", pontuacao);
        if (pontuacao > PlayerPrefs.GetInt("recorde"))
            PlayerPrefs.SetInt("recorde", pontuacao);
    }

    public void Faces()
    {
        if ((ChooseCharacter.perso == 0 && ControlGame.mode == 0) || (WaveChooseCharacter.persoWave == 0 && ControlGame.mode == 1))
        {
            faces[0].gameObject.SetActive(true);
        }

        if (ChooseCharacter.perso == 1 || WaveChooseCharacter.persoWave == 1)
        {
            faces[1].gameObject.SetActive(true);
        }

        if (ChooseCharacter.perso == 2 || WaveChooseCharacter.persoWave == 2)
        {
            faces[2].gameObject.SetActive(true);
        }

        if (ChooseCharacter.perso == 3 || WaveChooseCharacter.persoWave == 3)
        {
            faces[3].gameObject.SetActive(true);
        }
    }

    private void OnGUI()
    {
        if (PlayerControl.life == 0 || PlayerControl.life == -1)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
            hearts[3].SetActive(false);
        }

        if (PlayerControl.life == 1 || PlayerControl.life == 2)
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(true);
            hearts[2].SetActive(false);
            hearts[3].SetActive(false);
        }

        if (PlayerControl.life == 3 || PlayerControl.life == 4)
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(false);
            hearts[2].SetActive(true);
            hearts[3].SetActive(false);
        }

        if (PlayerControl.life == 5 || PlayerControl.life == 6)
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
            hearts[3].SetActive(true);
        }


        if (deathCountHUD == 0)
        {
            special[0].SetActive(true);
            special[1].SetActive(false);
            special[2].SetActive(false);
            special[3].SetActive(false);
            special[4].SetActive(false);
            special[5].SetActive(false);
            special[6].SetActive(false);
        }


        if (deathCountHUD == 1)
        {
            special[0].SetActive(false);
            special[1].SetActive(true);
            special[2].SetActive(false);
            special[3].SetActive(false);
            special[4].SetActive(false);
            special[5].SetActive(false);
            special[6].SetActive(false);
        }

        if (deathCountHUD == 2)
        {
            special[0].SetActive(false);
            special[1].SetActive(false);
            special[2].SetActive(true);
            special[3].SetActive(false);
            special[4].SetActive(false);
            special[5].SetActive(false);
            special[6].SetActive(false);
        }

        if (deathCountHUD == 3)
        {
            special[0].SetActive(false);
            special[1].SetActive(false);
            special[2].SetActive(false);
            special[3].SetActive(true);
            special[4].SetActive(false);
            special[5].SetActive(false);
            special[6].SetActive(false);
        }

        if (deathCountHUD == 4)
        {
            special[0].SetActive(false);
            special[1].SetActive(false);
            special[2].SetActive(false);
            special[3].SetActive(false);
            special[4].SetActive(true);
            special[5].SetActive(false);
            special[6].SetActive(false);
        }

        if (deathCountHUD == 5)
        {
            special[0].SetActive(false);
            special[1].SetActive(false);
            special[2].SetActive(false);
            special[3].SetActive(false);
            special[4].SetActive(false);
            special[5].SetActive(true);
            special[6].SetActive(false);
        }

        if (deathCountHUD >= 6)
        {
            Debug.Log("special pronto");
            special[0].SetActive(false);
            special[1].SetActive(false);
            special[2].SetActive(false);
            special[3].SetActive(false);
            special[4].SetActive(false);
            special[5].SetActive(false);
            special[6].SetActive(true);
        }        
    }
}