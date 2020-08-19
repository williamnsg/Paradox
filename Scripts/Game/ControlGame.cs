using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlGame : MonoBehaviour {

	public GameObject[] texts;
    public Text moneyTxt;
    public Text wavesTxt;
    public static int money,mode,deathParticlesCount;
    public bool cond;
    private GameObject deathParticles;

    public void Start()
	{
        money = 0;
        cond = true;
        SetNumText();
    }

    public void Update()
    {
        deathParticles = GameObject.FindWithTag("deathParticles");
        if (deathParticlesCount%10 == 0)
        {
            StartCoroutine(Destroy());
        }
        
        SetNumText();
    }

    public void SetNumText()
    {
        moneyTxt.text = "Gemas: " + money.ToString();
        wavesTxt.text = "Hordas: " + WaveEnemies.wavesCount.ToString();
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(deathParticles);
    }

    public void PlayOption()
    {        
        texts[0].gameObject.SetActive(false);//play
        texts[1].gameObject.SetActive(false);//controls
        texts[2].gameObject.SetActive(false);//exit
        texts[8].gameObject.SetActive(false);//instructions
        texts[3].gameObject.SetActive(true);//campaign
        texts[4].gameObject.SetActive(true);//wave
        texts[5].gameObject.SetActive(true);//back
    }

    public void ControlOption()
    {
        texts[0].gameObject.SetActive(false);//play
        texts[1].gameObject.SetActive(false);//controls
        texts[2].gameObject.SetActive(false);//exit
        texts[5].gameObject.SetActive(true);//back
        texts[6].gameObject.SetActive(true);//commands
        texts[7].gameObject.SetActive(true);//instructions
        texts[8].gameObject.SetActive(false);//instructions
    }

    public void BackMenu()
    {
        texts[0].gameObject.SetActive(true);//play
        texts[1].gameObject.SetActive(true);//controls
        texts[2].gameObject.SetActive(true);//exit
        texts[3].gameObject.SetActive(false);//campaign
        texts[4].gameObject.SetActive(false);//wave
        texts[5].gameObject.SetActive(false);//back
        texts[6].gameObject.SetActive(false);//commands
        texts[7].gameObject.SetActive(false);//controls Text
        texts[8].gameObject.SetActive(true);//instructions
        texts[9].gameObject.SetActive(false);//intructions text
    }

    public void Instructions()
    {
        texts[0].gameObject.SetActive(false);//play
        texts[1].gameObject.SetActive(false);//controls
        texts[2].gameObject.SetActive(false);//exit
        texts[5].gameObject.SetActive(true);//back
        texts[6].gameObject.SetActive(false);//commands
        texts[7].gameObject.SetActive(false);//controls Text
        texts[8].gameObject.SetActive(false);//instructions
        texts[9].gameObject.SetActive(true);//intructions text
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Paradox");       
    }

    public void ChooseCharacterCampaign()
    {
        mode = 0;
        HUDLife.deathCountHUD = 0;
        InstantiateBoss.deathCountBoss = 0;
        Debug.Log("mode0");
        SceneManager.LoadScene("ChooseCharacter");
    }

    public void ChooseCharacterWave()
    {
        mode = 1;
        HUDLife.deathCountHUD = 0;
        InstantiateBoss.deathCountBoss = 0;
        Debug.Log("mode1");
        SceneManager.LoadScene("WaveChooseCharacter");        
    }

    public void CampaignLevelAgain()
    {
        HUDLife.deathCountHUD = 0;
        InstantiateBoss.deathCountBoss = 0;
        SceneManager.LoadScene("Level01");
    }

    public void WaveLevelAgain()
    {
        PlayerControl.hp = 100;
        HUDLife.deathCountHUD = 0;
        InstantiateBoss.deathCountBoss = 0;
        SceneManager.LoadScene("LevelWave");
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    IEnumerator TheEnd()
    {      
       if (BossAI.end == 1)
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("TheEnd");
        }
    }	

    public void Final()
    {
        SceneManager.LoadScene("TheEnd");
    }
}
