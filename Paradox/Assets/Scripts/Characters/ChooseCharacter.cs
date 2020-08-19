using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCharacter : MonoBehaviour
{
	public static int perso;
    
    
    public void JoeDuck()
	{
        if (ControlGame.mode == 0)
        {
            perso = 0;
            SceneManager.LoadScene("Cutscene");
            Debug.Log("Instanciou");
        }        
    }
    
	public void Josuina()
	{
        if (ControlGame.mode == 0)
        {            
            perso = 1;
            SceneManager.LoadScene("Cutscene");
            Debug.Log("Instanciou");
        }       
    }

    public void Miguelito()
    {
        if (ControlGame.mode == 0)
        {            
            perso = 2;
            SceneManager.LoadScene("Cutscene");
            Debug.Log("Instanciou");
        }    
    }

    public void Willina()
    {
        if (ControlGame.mode == 0)
        {            
            perso = 3;
            SceneManager.LoadScene("Cutscene");
            Debug.Log("Instanciou");
        }        
    }
}