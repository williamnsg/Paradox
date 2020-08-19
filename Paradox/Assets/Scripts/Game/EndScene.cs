using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

    private int word,word2;
    public Text lastTxt, lastTxt2, lastTxt3;
    public string lastText,lastText2, lastText3;
    public GameObject create,logo;

	void Start ()
    {
        StartCoroutine(LastWords(lastText));
        StartCoroutine(LastWords2(lastText2));
        StartCoroutine(LastWords3(lastText3));
        StartCoroutine(logoImage());
    }

    

    IEnumerator LastWords(string phrase)
    {
        word = 0;
        lastTxt.text = "";
        
        while (word < phrase.Length)
        {
            lastTxt.text += phrase[word];
            word += 1;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3f);
        lastTxt.gameObject.SetActive(false);
        create.gameObject.SetActive(true);
    }

    IEnumerator LastWords2(string phrase)
    {
        yield return new WaitForSeconds(10f);        
        word = 0;
        lastTxt2.text = "";

        while (word < phrase.Length)
        {
            lastTxt2.text += phrase[word];
            word += 1;
            yield return new WaitForSeconds(0.2f);
        }        
        //Application.Quit();
    }

    IEnumerator LastWords3(string phrase2)
    {
        yield return new WaitForSeconds(9f);
        word2 = 0;
        lastTxt3.text = "";

        while (word2 < phrase2.Length)
        {
            lastTxt3.text += phrase2[word2];
            word2 += 1;
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Paradox");
    }

    IEnumerator logoImage()
    {
        for (int i = 0; i <200;i++)
        {
            yield return new WaitForSeconds(1f);
            logo.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            logo.gameObject.SetActive(true);
        }
    }
}
