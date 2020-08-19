using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoryScript : MonoBehaviour {

    public Text yellowTxt,yellowTxt2;
    public Text redTxt,redTxt2;
    public Text playerTxt;
    public string yellowText,redText,playerText,redText2,yellowText2;
    private int word;
    public GameObject red, yellow,box;
    public GameObject[] players;
   

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(YellowWords(yellowText));
        StartCoroutine(RedWords(redText));
        StartCoroutine(PlayerWords(playerText));
        StartCoroutine(RedWords2(redText2));
        StartCoroutine(YellowWords2(yellowText2));      
        StartCoroutine(Face());
        StartCoroutine(ChangeScene());
    }


    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(31);
        SceneManager.LoadScene("Level01");
    }   

    IEnumerator YellowWords(string phrase)
    {
        word = 0;       
        yellowTxt.text = "";
        
        while (word < phrase.Length)
        {
            yellowTxt.text += phrase[word];
            word += 1;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.6f);
        yellowTxt.gameObject.SetActive(false);
    }

    IEnumerator YellowWords2(string phrase)
    {
        yield return new WaitForSeconds(25);
        yellowTxt.gameObject.SetActive(true);
        yellow.gameObject.SetActive(true);
        word = 0;
        yellowTxt2.text = "";

        while (word < phrase.Length)
        {
            yellowTxt2.text += phrase[word];
            word += 1;
            yield return new WaitForSeconds(0.1f);
        }        
        
    }

    IEnumerator RedWords(string phrase)
    {
        yield return new WaitForSeconds(2.4f);
        word = 0;
        redTxt.text = "";

        while (word < phrase.Length)
        {
            redTxt.text += phrase[word];
            word += 1;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.6f);
        redTxt.gameObject.SetActive(false);
        box.gameObject.SetActive(false);        
    }

    IEnumerator RedWords2(string phrase)
    {
        yield return new WaitForSeconds(20);
        redTxt.gameObject.SetActive(true);
        red.gameObject.SetActive(true);
        box.gameObject.SetActive(true);
        word = 0;
        redTxt2.text = "";

        while (word < phrase.Length)
        {
            redTxt2.text += phrase[word];
            word += 1;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        red.gameObject.SetActive(false);
        redTxt.gameObject.SetActive(false);         
    }

    IEnumerator PlayerWords(string phrase)
    {
        yield return new WaitForSeconds(13f);
        word = 0;
        playerTxt.text = "";

        while (word < phrase.Length)
        {
            playerTxt.text += phrase[word];
            word += 1;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.6f);               
    }

    IEnumerator Face()
    {
        yellow.gameObject.SetActive(true);
        red.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.4f);
        yellow.gameObject.SetActive(false);
        red.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        red.gameObject.SetActive(false);        

        yield return new WaitForSeconds(7f);
        if (ChooseCharacter.perso == 0)
        {
            box.gameObject.SetActive(true);
            players[0].gameObject.SetActive(true);            
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);
            yield return new WaitForSeconds(7.5f);         
            playerTxt.gameObject.SetActive(false);
            players[0].gameObject.SetActive(false);
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);            
        }

        if (ChooseCharacter.perso == 1)
        {
            box.gameObject.SetActive(true);
            players[0].gameObject.SetActive(false);
            players[1].gameObject.SetActive(true);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);
            yield return new WaitForSeconds(7.5f);
            playerTxt.gameObject.SetActive(false);
            players[0].gameObject.SetActive(false);
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);
        }

        if (ChooseCharacter.perso == 2)
        {
            box.gameObject.SetActive(true);
            players[0].gameObject.SetActive(false);
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(true);
            players[3].gameObject.SetActive(false);
            yield return new WaitForSeconds(7.5f);
            playerTxt.gameObject.SetActive(false);            
            players[0].gameObject.SetActive(false);
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);

        }

        if (ChooseCharacter.perso == 3)
        {
            box.gameObject.SetActive(true);
            players[0].gameObject.SetActive(false);
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(true);
            yield return new WaitForSeconds(7.5f);
            playerTxt.gameObject.SetActive(false);            
            players[0].gameObject.SetActive(false);
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);
        }        
    }
}
