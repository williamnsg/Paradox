using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmanAI : MonoBehaviour {

    //PREFERENCE SWORDMAN YELLOW
    public Transform myTransform;
    public Transform player;    
    public float distanceCond,turn;//Distance condition to start follow
    private float distance;//Distance of the player and the enemy
    public Animator anime;//Enemy animator
    public bool walk, punch,rage;
    public Transform slash;
    public int enemyLife;
    public float enemyHp;
    public static float playerDamage;
    public GameObject deathParticles,gemFab;
    public AudioSource source;
    public AudioClip slashEffect;
	public GameObject HP0;
	public GameObject HP1;

    public EnemyState currentState;
    public enum EnemyState
    {
        point,
        followplayer,
        stopped
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        enemyLife = 2;
        enemyHp = 100;
        walk = true;
        punch = true;
        player = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.followplayer;
    }


    void Update()
    {

		if (ControlGame.mode == 0)
		{
			if (enemyLife == 1) {
				HP0.SetActive (true);
				HP1.SetActive (false);
			}

			if (enemyLife == 2) {
				HP0.SetActive (false);
				HP1.SetActive (true);
			}
		}

		if (ControlGame.mode == 1)
		{
			if (enemyHp == 50)
			{
				HP0.SetActive (true);
				HP1.SetActive (false);
			}

			if (enemyHp == 100) 
			{
				HP0.SetActive (false);
				HP1.SetActive (true);
			}
		}

        if (enemyLife == 0 && ControlGame.mode == 0)
        {
            InstantiateBoss.deathCountBoss += 1;
            Dead();
        }
    
        if (enemyHp == 0 && ControlGame.mode == 1)
        {
            WaveEnemies.enemyWaveDeath += 0.5f;
            Debug.Log(WaveEnemies.enemyWaveDeath);
            Instantiate(gemFab, myTransform.position, myTransform.rotation);
            Dead();
        }
      
        if (PlayerControl.hp == 0 || PlayerControl.life == 0)
        {
            currentState = EnemyState.stopped;
        }

        if (PlayerControl.defenceOn == true)
        {
            currentState = EnemyState.stopped;
        }
        else
        {
            currentState = EnemyState.followplayer;
        }

        switch (currentState)
        {
            case EnemyState.point:

                Point();
                break;

            case EnemyState.followplayer:

                FollowPlayer();
                break;

            case EnemyState.stopped:

                Stopped();
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "punch")
        {
            if (ControlGame.mode == 0)
            {
                enemyLife = enemyLife - 1;
            }

            if (ControlGame.mode == 1)
            {
                enemyHp = enemyHp - playerDamage;
            }
            StartCoroutine(FDamage());
        }

        if (other.gameObject.tag == "special")
        {
            if (ControlGame.mode == 0)
            {
               enemyLife = 0;
            }

            if (ControlGame.mode == 1)
            {
                enemyHp = 0;
            }
            StartCoroutine(FDamage());
            
                       
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && punch == true)//Condition to attack
        {
            StartCoroutine(eAttack());
        }
    }

    IEnumerator eAttack()//Function to control the animation time of attack - Função que controla o tempo de animação do ataque
    {
        yield return new WaitForSeconds(0.2f);        
        source.PlayOneShot(slashEffect, 0.3f);
        punch = false;
        anime.SetBool("punch", true);
        slash.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        anime.SetBool("punch", false);
        punch = true;
        slash.gameObject.SetActive(false);       
    }

    public void Dead()
    {
            HUDLife.deathCountHUD = HUDLife.deathCountHUD + 1;
            ControlGame.deathParticlesCount += 1;
            Instantiate(deathParticles, myTransform.position, myTransform.rotation);
            Debug.Log("Inimigo morreu");
            HUDLife.pontuacao += 10;
            Destroy(gameObject);
     }
    

    public void Stopped()
    {
        anime.SetBool("walk", false);
    }
    public void Point()
    {
        transform.localScale = new Vector3(turn, turn, turn);        
        transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("point2").transform.position, Time.deltaTime * 0.3f);
        anime.SetBool("walk", walk);

    }

    public void FollowPlayer()//Function to follow the player
    {
        distance = myTransform.position.x - player.transform.position.x; //Calculate the distance to follow - Calcular a distancia para seguir o player

        if (myTransform.position.x < player.transform.position.x)//Change local scale to turn the look side of the enemy (right side) - Para virar o inimigo de acordo com a posição do player ( virar para direita)
        {
                transform.localScale = new Vector3(turn, turn, turn);

            if (distance > -distanceCond)//Right player position distance to start follow (Condition) - Condição para começar a andar dependendo da distancia pela direita
            {
                transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime * 1);
                anime.SetBool("walk", walk);

            }
            else
            {
                //Stop follow if one of the conditions is not true - Parar de seguir caso as condições/distancias não sejam verdadeiras
                transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime * 0);
                anime.SetBool("walk", false);
            }

        }

        if (myTransform.position.x > player.transform.position.x)//Change local scale to turn the look side of the enemy (left side) - Para virar o inimigo de acordo com a posição do player ( virar para esquerda)
        {
                transform.localScale = new Vector3(-turn, turn, turn);

            if (distance < distanceCond)//Left player position distance to start follow (Condition) - Condição para começar a andar dependendo da distancia pela esquerda
            {
                transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime * 1);
                anime.SetBool("rage",false);
                anime.SetBool("walk", walk);
            }
            else
            {
                //Stop follow if one of the conditions is not true - Parar de seguir caso as condições/distancias não sejam verdadeiras
                transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime * 0);
                anime.SetBool("walk", false);
            }
        }
    }//Follow

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
}
    