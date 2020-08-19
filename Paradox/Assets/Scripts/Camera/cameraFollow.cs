using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	[SerializeField]
	private  float maximoX;
	[SerializeField]
	private  float minimoX;
	[SerializeField]
	private  float minimoY;
	[SerializeField]
	private  float maximoY;
    public float cameraWait;  
    
    public Transform player;
	void Start ()
    {
        player = GameObject.FindWithTag("Player").transform;
		StartCoroutine(cameraMax());
    }
	
	// Update is called once per frame
	void Update ()
    {
		transform.position = new Vector3(Mathf.Clamp(player.position.x,minimoX,maximoX),Mathf.Clamp(player.position.y,minimoY,maximoY),transform.position.z);
	}

	IEnumerator cameraMax()
    {	
		yield return new WaitForSeconds (cameraWait);
		maximoX = 2.8f;
    }
}
