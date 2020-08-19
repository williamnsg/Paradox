using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameOver : MonoBehaviour {


	// Use this for initialization
	public Text pontos;
	public Text recorde;


	void Start ()
	{
		pontos.text = PlayerPrefs.GetInt ("pontuacao").ToString ();
		recorde.text = PlayerPrefs.GetInt ("recorde").ToString ();
	}
		

	// Update is called once per frame
	void Update ()
	{
		
	}
}
