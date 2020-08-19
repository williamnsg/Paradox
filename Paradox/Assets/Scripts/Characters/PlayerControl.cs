using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    public static float hp, damageBust;
    public static int life;
    public float maxZ;
    public float minZ;
    public float maxX;
    public float minX;
    public float speedX, speedZ, turnScale;
    public Animator anime;
    public bool walk, punch, defence, special, damage, death;
    public static bool varKeys, d,defenceOn;
    public Transform attack;
    public Transform specialFab;
    public AudioSource source;
    public AudioClip playerPunch;
    public AudioClip playerSpecial;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        SwordmanAI.playerDamage = 50;
        SwordmanAI2.playerDamage = 50;
        hp = 100;
        life = 6;
        damageBust = 1;
        punch = true;
        defence = true;
        special = true;
        death = true;
        varKeys = true;
        d = true;
    }


    void Update()
    {
        hp = (hp * 100) / 100;
        StartCoroutine(Dead());
        Movimantation();
        PunchDefence();
        StartCoroutine(maxWalk());
    }

    void Movimantation()
    {
        if (Input.GetKey("left") && varKeys == true && MerchantStore.storeAtive == false)
        {
            transform.position = new Vector3(transform.position.x - speedX, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-turnScale, 0.8f, turnScale);
            walk = true;
        }
        else
        {
            walk = false;
        }


        if (Input.GetKey("right") && varKeys == true && MerchantStore.storeAtive == false)
        {
            transform.position = new Vector3(transform.position.x + speedX, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(turnScale, 0.8f, turnScale);
            walk = true;
        }


        if (Input.GetKey("up") && varKeys == true && MerchantStore.storeAtive == false && MerchantStore.storeAtive == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speedZ);
            walk = true;
        }


        if (Input.GetKey("down") && varKeys == true && MerchantStore.storeAtive == false )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speedZ);
            walk = true;
        }


        if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }


        if (transform.position.z < minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }


        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }


        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        anime.SetBool("walk", walk);

    }

    IEnumerator Dead()
    {
        if (ControlGame.mode == 0)
        {
            if (life <= 0)
            {
                anime.SetBool("death", death);
                varKeys = false;
                GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Player morreu");
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("GameOverCampaign");
            }
        }

        if (ControlGame.mode == 1)
        {
            if (hp <= 0)
            {
                anime.SetBool("death", death);
                varKeys = false;
                Debug.Log("Player morreu");
                GetComponent<BoxCollider>().enabled = false;
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("GameOverWave");
            }
        }
    }

    public void PunchDefence()
    {
        if (Input.GetKeyDown("f") && punch == true && varKeys == true)
        {
            StartCoroutine(Fattack());
            SoundsEffects();
        }

        if (Input.GetKey("g") && defence == true && d == true && MerchantStore.storeAtive == false)
        {
            defenceOn = true;
            anime.SetBool("defence", true);
            varKeys = false;
        }
        else
        {
            defenceOn = false;
            anime.SetBool("defence", false);
            varKeys = true;
        }

        if (Input.GetKeyDown("space") && special == true && HUDLife.deathCountHUD >= 6)
        {
            StartCoroutine(SpecialAttack());
            SoundsEffects();
            HUDLife.deathCountHUD = 0;
        }
    }    

    void SoundsEffects()
    {
        if (Input.GetKeyDown("f"))
        {
            source.PlayOneShot(playerPunch, 0.7f);
        }

        if (Input.GetKeyDown("space"))
        {
            source.PlayOneShot(playerSpecial, 0.7f);
        }
    }

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.tag == "enemyPunch")
        {
            if (ControlGame.mode == 0)
            {
                if (defenceOn == true)
                {
                    life = life - 1 + 1;
                }
                else
                {
                    life = life - 1;
                }
            }

            if (ControlGame.mode == 1)
            {
                if (defenceOn == true)
                {
                    hp = hp - 5;
                }else
                {
                    hp = hp - 10;
                }
            }
           
            StartCoroutine(FDamage());
        }

        if (Collider.gameObject.tag == "bossPunch")
        {
            if (ControlGame.mode == 0)
            {
                life = life - 2;
            }

            if (ControlGame.mode == 1)
            {
                hp = hp - 30;
            }
            StartCoroutine(FDamage());
        }
    }

    IEnumerator SpecialAttack()
    {
        anime.SetBool("special", true);
        specialFab.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("Especial");
        anime.SetBool("special", false);
        specialFab.gameObject.SetActive(false);
    }

    IEnumerator Fattack()
    {
        punch = false;
        anime.SetBool("punch", true);
        attack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        anime.SetBool("punch", false);
        punch = true;
        attack.gameObject.SetActive(false);
    }

    IEnumerator FDamage()
    {
        for (int i = 0; i < 4; i++)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator maxWalk()
    {
        if (ControlGame.mode == 0)
        {
            yield return new WaitForSeconds(5);
            maxX = 3.9f;
        }

        if (ControlGame.mode == 1)
        {
            yield return new WaitForSeconds(0);
            maxX = 3.9f;
        }
    }    
}