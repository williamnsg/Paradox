using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveChooseCharacter : MonoBehaviour
{
	public static int persoWave;

    private void Start()
    {
       
    }

    public void JoeDuck()
	{
        if (ControlGame.mode == 1)
        {
            persoWave = 0;
            SceneManager.LoadScene("LevelWave");
            Debug.Log("Instanciou");
        }        
    }
    
	public void Josuina()
	{
        if (ControlGame.mode == 1)
        {
            persoWave = 1;
            SceneManager.LoadScene("LevelWave");
            Debug.Log("Instanciou");
        }       
    }

    public void Miguelito()
    {
        if (ControlGame.mode == 1)
        {
            persoWave = 2;
            SceneManager.LoadScene("LevelWave");
            Debug.Log("Instanciou");
        }    
    }

    public void Willina()
    {
        if (ControlGame.mode == 1)
        {
            persoWave = 3;
            SceneManager.LoadScene("LevelWave");
            Debug.Log("Instanciou");
        }        
    }
}