using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateBoss : MonoBehaviour {

    public GameObject bossFab, pointFab, message;
    public bool cond, inst;
    public Vector3 bossPosition, pointPosition;
    public static int deathCountBoss;

    void Start ()
    {
        pointPosition = new Vector3(Random.Range(0, 3f), 0.7f, Random.Range(0.3f, 1f));
        cond = true;       
    }
	
	
	void Update ()
    {
        StartCoroutine(Message());
        if (ControlGame.mode == 0)
        {
            BossInstantiate();
        }

        if (BossAI.bossDead == true)
        {
            StartCoroutine(Delay());
        }
    }

    public void BossInstantiate()
    {
        if (deathCountBoss == 7 && cond == true)
        {
            inst = true;
            Debug.Log("boss");
            cond = false;
            Instantiate(bossFab, bossPosition, bossFab.transform.rotation);
            Instantiate(pointFab, pointPosition, pointFab.transform.rotation);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("TheEnd");
    }

    IEnumerator Message()
    {
        if (inst == true)
        {
                inst = false;   
                message.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                message.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                message.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                message.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                message.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                message.gameObject.SetActive(false);
        }
    }
}
