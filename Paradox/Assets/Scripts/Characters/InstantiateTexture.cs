using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class InstantiateTexture : MonoBehaviour {

	public GameObject joe,josuina,miguelito,willina,thunder;
	public Vector3 positionInstantiate,thunderPosition;
    public AudioClip thunderEffect;
    public AudioSource source;
    
	void Start () 
	{
        StartCoroutine(Delay());        
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        source.PlayOneShot(thunderEffect,1);
        Instantiate(thunder, thunderPosition, joe.transform.rotation);

        if (ChooseCharacter.perso == 0)
        {            
            yield return new WaitForSeconds(4);
            Instantiate(joe, positionInstantiate, joe.transform.rotation);            
        }

        if (ChooseCharacter.perso == 1)
        {
            yield return new WaitForSeconds(4);
            Instantiate(josuina, positionInstantiate, josuina.transform.rotation);           
        }

        if (ChooseCharacter.perso == 2)
        {
            yield return new WaitForSeconds(4);
            Instantiate(miguelito, positionInstantiate, miguelito.transform.rotation);          
        }

        if (ChooseCharacter.perso == 3)
        {
            yield return new WaitForSeconds(4);
            Instantiate(willina, positionInstantiate, willina.transform.rotation);           
        }        
    }
}
