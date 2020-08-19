using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmanAIh : MonoBehaviour {

    //PREFERENCE SWORDMAN YELLOW
    public Transform myTransform;
    public Transform player;
    public Transform point;
    public float distanceCond,turn;//Distance condition to start follow
    private float distance;//Distance of the player and the enemy
    public Animator anime;//Enemy animator
    public bool walk, punch;
    public Transform slash;
    public int life;
    public GameObject deathParticles;
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
        walk = true;
        punch = true;
        player = GameObject.FindWithTag("Player").transform;
        point = GameObject.FindWithTag("point1").transform;
        currentState = EnemyState.point;
    }


    void Update()
    {

		if (life == 1)
		{
			HP0.SetActive(true);
			HP1.SetActive(false);
		}

		if (life == 2)
		{
			HP0.SetActive (false);
			HP1.SetActive (true);
		}

        Dead();

        if (PlayerControl.hp == 0 || PlayerControl.life == 0)
        {
            currentState = EnemyState.stopped;
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
        if (other.gameObject.tag == "point1")
        {
            currentState = EnemyState.followplayer;
            anime.SetBool("walk", false);
        }

        if (other.gameObject.tag == "punch")
        {
            StartCoroutine(FDamage());
            life = life - 1;
        }

        if (other.gameObject.tag == "special")
        {
            StartCoroutine(FDamage());
            life = 0;
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
        source.PlayOneShot(slashEffect, 0.5f);
        punch = false;
        anime.SetBool("punch", true);
        slash.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        anime.SetBool("punch", false);
        punch = true;
        slash.gameObject.SetActive(false);
    }

    public void Dead()
    {
        if (life == 0)
        {
            HUDLife.deathCountHUD = HUDLife.deathCountHUD + 1;
            InstantiateBoss.deathCountBoss = InstantiateBoss.deathCountBoss + 1;
            Instantiate(deathParticles, myTransform.position, myTransform.rotation);
            Debug.Log("Inimigo morreu");
            HUDLife.pontuacao += 10;
            Destroy(gameObject);            
        }
    }

    public void Stopped()
    {
        anime.SetBool("walk", false);
    }
    public void Point()
    {
        transform.localScale = new Vector3(turn, turn, turn);        
        transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("point1").transform.position, Time.deltaTime * 0.6f);
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
    