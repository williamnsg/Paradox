using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemies : MonoBehaviour {

    public GameObject[] enemies;
    public GameObject[] messages;
    public GameObject bossFab,pointFab,storeName;
    public static float enemyWaveDeath,bossCount;
    public bool spawn,damBust;
    public static bool bossOn;
    public int enemyCount,lifePlus;
    public static int wavesCount;
    private Vector3 position, position2, position3;
    public Vector3 bossPosition;

    /* Message[0] preference for waves warning
     * Message[1] preference for boss warning
     * Message[2] preference for first warning*/

    private void Start()
    {
        lifePlus = 0;
        spawn = true;
        bossCount = 0;
        bossOn = false;
        wavesCount = 0;
        enemyWaveDeath = 0;
    }

    void Update()
    {
        if (spawn == true)
        {
            wavesCount += 1;
            Debug.Log(wavesCount);
            StartCoroutine(SpawnEnemy());
            spawn = false;
        }

        if (enemyWaveDeath == enemyCount && bossOn == false)
        {
            enemyWaveDeath = 0;
            enemyCount += 1;
            StartCoroutine(ProxHorda());
        }

        if (wavesCount%3 == 0 && wavesCount >= 1)
        {
            MerchantStore.waves = true;
            storeName.gameObject.SetActive(true);
        }
        else
        {
            storeName.gameObject.SetActive(false);
            MerchantStore.waves = false;
        }

        if (wavesCount%3 == 0 && bossOn == false && bossCount == 0) 
        {
            InstantiateBoss();
        }

        if (wavesCount%9 == 0 && lifePlus == 0 && bossOn == true)
        {
            lifePlus = 1;
            Debug.Log("hpBoss aumentado");
            BossAI.hpBoss += 100;
        }
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            position = new Vector3(Random.Range(-0.58f, 0.7f), 0.7f, Random.Range(0.067f, 1f));
            position2 = new Vector3(Random.Range(2.9f, 3.9f), 0.7f, Random.Range(0.067f, 1f));
            Instantiate(enemies[0], position, enemies[0].transform.rotation);
            Instantiate(enemies[1], position2, enemies[1].transform.rotation);
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator ProxHorda()
    {
        messages[0].gameObject.SetActive(true);
        Debug.Log("proxhorda");
        yield return new WaitForSeconds(10);
        spawn = true;
        bossCount = 0;
        lifePlus = 0;
        messages[0].gameObject.SetActive(false);
    }

    public void InstantiateBoss()
    {
        bossCount = 1;
        position3 = new Vector3(Random.Range(0, 3f), 0.7f, Random.Range(0.3f, 1f));
        StartCoroutine(BossWarning());
        Instantiate(bossFab, bossPosition, bossFab.transform.rotation);
        Instantiate(pointFab, position3, pointFab.transform.rotation);
    }

    IEnumerator BossWarning()
    {
        messages[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        messages[1].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        messages[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        messages[1].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        messages[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        messages[1].gameObject.SetActive(false);
    }
}
