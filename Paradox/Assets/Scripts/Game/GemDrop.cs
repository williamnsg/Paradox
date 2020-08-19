using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ControlGame.money += 1;
            Debug.Log("money " + ControlGame.money);
            Destroy(gameObject);
        }
    }
}
