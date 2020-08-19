using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateRed
    : MonoBehaviour {

    public Vector3 positionFirstRed,positionOtherRed, positionOtherRed2;
    public GameObject redSwordman;
    public int enemyCount;
    public float spawnWait;
    public float startWait;
    public bool cond;

    void Start()
    {
        cond = true;
        FirstRed();
        StartCoroutine(SpawnRed());        
    }

    public void Update()
    {
        SpawnLastRed();
    }
    public void FirstRed()
    {
        Instantiate(redSwordman, positionFirstRed, Quaternion.Euler(0, 0, 0));
    }

    IEnumerator SpawnRed()
    {
        yield return new WaitForSeconds(startWait);
       
            for (int i = 0; i < enemyCount; i++)
            {            
                Instantiate(redSwordman, positionOtherRed, Quaternion.Euler(0, 0, 0));                
                yield return new WaitForSeconds(spawnWait);
                positionOtherRed.x = positionOtherRed.x - 0.1f;
                positionOtherRed.z = positionOtherRed.z - 0.5f;
        }
    }

    public void SpawnLastRed()
    {        
        if (HUDLife.deathCountHUD == 6 && cond == true)
        {           
            Instantiate(redSwordman, positionOtherRed2, Quaternion.Euler(0, 0, 0));
            cond = false;
        }       
    }
}
