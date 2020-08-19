using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour {

    public Transform player;//Player transform
    public Transform myTransform;//Enemy transform
    public Transform point;
    public Animator anime;//Enemy animator
    public bool walk, punch, follow, laugh,cond;
    public static bool bossDead;
    public float turn;//number of local scale
    public float distanceCond;//Distance condition to start follow
    private float distance;//Distance of the player and the enemy
    public Transform crash,dustParticle;
    public int life;
    public static int end,playerDamage, hpBoss;
    public int[] random;
    public AudioSource source;
    public AudioClip bossPunch;
	public GameObject[] HP;
    public GameObject deathParticles;
   
	

    public EnemyState currentState;
    public enum EnemyState
    {         
        idle,
        point,
        followplayer,
        entry
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start ()
    {
        WaveEnemies.bossOn = true;
        player = GameObject.FindWithTag("Player").transform; //target the player
        point = GameObject.FindWithTag("pointBoss").transform;        
        currentState = EnemyState.point;
        playerDamage = 50;
        bossDead = false;
        walk = true;
        follow = true;
        laugh = true;
        punch = true;
        life = 5;
        hpBoss = 250; 
    }

    // Update is called once per frame
    void Update ()
    {
        if (ControlGame.mode == 0)
        {
            LifeBar();
        }

        if (ControlGame.mode == 0 && life <= 0)
        {
            Dead();
        }

        if (ControlGame.mode == 1 && hpBoss <= 0)
        {
            Dead();
        }

        switch (currentState)
        {
            case EnemyState.idle:

                StartCoroutine(Idle());
                break;

            case EnemyState.point:

                Point();
                break;

            case EnemyState.followplayer:

                FollowPlayer();
                break;

            case EnemyState.entry:

                StartCoroutine(Entry());
                break;

            default:
                break;
        }       
    }

    public void LifeBar()
    {
        if (life == 1)
        {
            HP[0].SetActive(true);
            HP[1].SetActive(false);
            HP[2].SetActive(false);
            HP[3].SetActive(false);
            HP[4].SetActive(false);
        }

        if (life == 2)
        {
            HP[0].SetActive(false);
            HP[1].SetActive(true);
            HP[2].SetActive(false);
            HP[3].SetActive(false);
            HP[4].SetActive(false);

        }

        if (life == 3)
        {
            HP[0].SetActive(false);
            HP[1].SetActive(false);
            HP[2].SetActive(true);
            HP[3].SetActive(false);
            HP[4].SetActive(false);
        }

        if (life == 4)
        {
            HP[0].SetActive(false);
            HP[1].SetActive(false);
            HP[2].SetActive(false);
            HP[3].SetActive(true);
            HP[4].SetActive(false);
        }

        if (life == 5)
        {
            HP[0].SetActive(false);
            HP[1].SetActive(false);
            HP[2].SetActive(false);
            HP[3].SetActive(false);
            HP[4].SetActive(true);
        }
    }

    public void Dead()
    {
            bossDead = true;
            WaveEnemies.bossOn = false;
            Instantiate(deathParticles,myTransform.position,myTransform.rotation);
            HUDLife.deathCountHUD = HUDLife.deathCountHUD + 1;
            Debug.Log("Boss morreu");
            HUDLife.pontuacao += 50;
            Destroy(gameObject);    
    }

    

    public void Point()
    {
        transform.localScale = new Vector3(-turn, turn, turn);
        transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("pointBoss").transform.position, Time.deltaTime * 0.2f);
        anime.SetBool("walk", walk);
    }

    IEnumerator Entry()
    {
        anime.SetBool("laugh", laugh);
        yield return new WaitForSeconds(3f);
        currentState = EnemyState.followplayer;
    }

    public void FollowPlayer()//Function to follow the player
    {
        anime.SetBool("laugh", false);
        distance = myTransform.position.x - player.transform.position.x; //Calculate the distance to follow - Calcular a distancia para seguir o player

        if (myTransform.position.x < player.transform.position.x && follow == true)//Change local scale to turn the look side of the enemy (right side) - Para virar o inimigo de acordo com a posição do player ( virar para direita)
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

        if (myTransform.position.x > player.transform.position.x && follow == true)//Change local scale to turn the look side of the enemy (left side) - Para virar o inimigo de acordo com a posição do player ( virar para esquerda)
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

    }//End of FollowPlayer


    private void OnTriggerEnter(Collider Collider)//Function to verify if player are punching the enemy and release life 
    {
        if (Collider.gameObject.tag == "punch")
        {
            if (ControlGame.mode == 0)
            {
                life = life - 1;
                StartCoroutine(FDamage());
            }

            if (ControlGame.mode == 1)
            {
                hpBoss = hpBoss - playerDamage;
                StartCoroutine(FDamage());
            }
        }

        if (Collider.gameObject.tag == "special")
        {
            StartCoroutine(FDamage());
            life = life - 3;
            hpBoss = hpBoss - 30;
        }

        if (Collider.gameObject.tag == "pointBoss")
        {            
            currentState = EnemyState.entry;
        }

        if (Collider.gameObject.tag == "Player")
        {
            currentState = EnemyState.followplayer;
        }
    }
   

    private void OnTriggerStay(Collider other)//Function to start attack the player on collision - Função para atacar o jogador
    {
        if (other.gameObject.tag == "Player" && punch == true)//Condition to attack
        {
            StartCoroutine(eAttack());
        }
    }

    IEnumerator eAttack()//Function to control the animation time of attack - Função que controla o tempo de animação do ataque
    {       
        yield return new WaitForSeconds(0.8f);
        source.PlayOneShot(bossPunch, 0.7f);
        punch = false;
        dustParticle.gameObject.SetActive(true);
        anime.SetBool("punch", true);        
        crash.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        anime.SetBool("punch", false);
        crash.gameObject.SetActive(false);
        dustParticle.gameObject.SetActive(false);
        punch = true;             
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

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(4);
        anime.SetBool("laugh", laugh);
        yield return new WaitForSeconds(0.7f);
        anime.SetBool("laugh", false);
    }
}
