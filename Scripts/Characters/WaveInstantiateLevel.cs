using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInstantiateLevel : MonoBehaviour {

	public GameObject[] players;
    public GameObject point;
	public Vector3 positionInstantiate,pointPosition;
    
	void Start () 
	{
        Instantiate(point, pointPosition, point.transform.rotation);

		if(WaveChooseCharacter.persoWave == 0)
		{
			Instantiate(players[0],positionInstantiate, players[0].transform.rotation );
		}

		if(WaveChooseCharacter.persoWave == 1)
		{
			Instantiate(players[1], positionInstantiate, players[0].transform.rotation );
		}

        if (WaveChooseCharacter.persoWave == 2)
        {
            Instantiate(players[2], positionInstantiate, players[0].transform.rotation);
        }

        if (WaveChooseCharacter.persoWave == 3)
        {
            Instantiate(players[3], positionInstantiate, players[0].transform.rotation);
        }
    }
}
