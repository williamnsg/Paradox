using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMovie : MonoBehaviour {

    

    private void Start()
    {
        StartCoroutine(Change());
    }
        
    IEnumerator Change()
    {
        yield return new WaitForSeconds(148);
        SceneManager.LoadScene("Paradox");
    }
}
