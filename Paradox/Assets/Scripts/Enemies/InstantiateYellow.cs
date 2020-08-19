using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateYellow : MonoBehaviour {

	public Vector3 positionFirstYellow,positionOtherYellow;    
    public GameObject[] yellowSwordman;   
    public int enemyCount;
    public float spawnWait;
    public float startWait;

    void Start()
    {
        FirstYellow();     
        StartCoroutine(SpawnYellow());  
    }

    public void FirstYellow()
    {
        Instantiate(yellowSwordman[0], positionFirstYellow, Quaternion.Euler(0, 0, 0));
    }

    IEnumerator SpawnYellow()
    {
        yield return new WaitForSeconds(startWait);

            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(yellowSwordman[1], positionOtherYellow, Quaternion.Euler(0, 0, 0));
                yield return new WaitForSeconds(spawnWait);
                positionOtherYellow.x = positionOtherYellow.x + 0.576f;
                positionOtherYellow.z = positionOtherYellow.z + 0.734f;
            }                       
    }    
}
