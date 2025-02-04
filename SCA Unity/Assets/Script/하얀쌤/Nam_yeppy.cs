using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Leguar.LowHealth;

public class Nam_yeppy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private Animator anim;

    public float playerViewAngle = 45f; // 플레이어가 몬스터를 바라보는 감지 각도
    private bool isBeingWatched = false; // 플레이어가 몬스터를 바라보고 있는지 여부
    private bool hasPlayedSound = false; // 오디오 중복 실행 방지용
    private bool isAttacking = false;

    public AudioSource what;

    public GameObject playerCam;
    public GameObject attackCam;

    public LowHealthController shaderControllerScript;
    private float currentHP;

    void Start()
    {
        //Debug.Log(PlayerStats.Instance.Health);
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.isStopped = false;
        //anim.SetBool("isWalk", true);
    }

    void Update()
    {
        DetectPlayerView(); // 플레이어가 몬스터를 바라보고 있는지 감지

        if (isBeingWatched)
        {
            anim.applyRootMotion = true;
            agent.isStopped = true; // 이동 멈춤
            //anim.SetBool("isWalk", false);
            anim.SetBool("isLooking", true);

            // 사운드 중복 실행 방지
            if (!hasPlayedSound)
            {
                what.Play();
                hasPlayedSound = true;
            }
        }
        else
        {
            anim.applyRootMotion = false;
            agent.isStopped = false; // 다시 이동 시작
            //anim.SetBool("isWalk", true);
            anim.SetBool("isLooking", false);
            agent.destination = player.transform.position;

            // 다시 플레이어가 바라보지 않으면 사운드 재생 가능하도록 설정
            hasPlayedSound = false;
        }
    }

    void DetectPlayerView()
    {
        Vector3 directionToMonster = (transform.position - player.transform.position).normalized;
        float angle = Vector3.Angle(player.transform.forward, directionToMonster);

        isBeingWatched = angle < playerViewAngle / 2f;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && yeppy_player.catched == false)
        {

            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
            //yeppy_spawn.spawned = false;
            //Destroy(gameObject);
        }
    }
    private IEnumerator AttackPlayer()
    {
        
        isAttacking = true; // 공격 중 상태 설정
        agent.isStopped = true; //공격 중 이동 멈추기
        playerCam.SetActive(false);
        attackCam.SetActive(true);
        PlayerStats.Instance.TakeDamage(1);
        currentHP = PlayerStats.Instance.Health;
        yield return new WaitForSeconds(0.1f);

        shaderControllerScript.SetPlayerHealthSmoothly(0.2f, 3f);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(3f);

        

        isAttacking = false; 
        agent.isStopped = false; //공격 중 이동 멈추기
        playerCam.SetActive(true);
        attackCam.SetActive(false);
        yeppy_spawn.spawned = false;
        Destroy(gameObject);
    }
}

