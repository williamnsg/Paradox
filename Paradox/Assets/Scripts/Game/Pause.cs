using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Pause : MonoBehaviour {
    	
	void Update ()
    {
        Time.timeScale = 0;
        PlayerControl.varKeys = false;
        PlayerControl.d = false;
    }

   public void LeaveToMenu()
    {
        SceneManager.LoadScene("Paradox");
        HUDLife.deathCountHUD = 0;
        InstantiateBoss.deathCountBoss = 0;
        Time.timeScale = 1;
    }

   public void ResumeLevel()
    {
        Time.timeScale = 1;
        PlayerControl.varKeys = true;
        PlayerControl.d = true;
    }

   public void SelectCharacter()
    {
        if (ControlGame.mode == 0)
        {
            SceneManager.LoadScene("ChooseCharacter");
            HUDLife.deathCountHUD = 0;
            InstantiateBoss.deathCountBoss = 0;
            Time.timeScale = 1;
        }

        if (ControlGame.mode == 1)
        {
            SceneManager.LoadScene("WaveChooseCharacter");
            HUDLife.deathCountHUD = 0;
            InstantiateBoss.deathCountBoss = 0;
            Time.timeScale = 1;
        }
    }

    public void RepeatLevel()
    {
        HUDLife.deathCountHUD = 0;
        InstantiateBoss.deathCountBoss = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
