using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Leguar.LowHealth;

public class Nam_yeppy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private Animator anim;

    public float playerViewAngle = 45f; // �÷��̾ ���͸� �ٶ󺸴� ���� ����
    private bool isBeingWatched = false; // �÷��̾ ���͸� �ٶ󺸰� �ִ��� ����
    private bool hasPlayedSound = false; // ����� �ߺ� ���� ������
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
        DetectPlayerView(); // �÷��̾ ���͸� �ٶ󺸰� �ִ��� ����

        if (isBeingWatched)
        {
            anim.applyRootMotion = true;
            agent.isStopped = true; // �̵� ����
            //anim.SetBool("isWalk", false);
            anim.SetBool("isLooking", true);

            // ���� �ߺ� ���� ����
            if (!hasPlayedSound)
            {
                what.Play();
                hasPlayedSound = true;
            }
        }
        else
        {
            anim.applyRootMotion = false;
            agent.isStopped = false; // �ٽ� �̵� ����
            //anim.SetBool("isWalk", true);
            anim.SetBool("isLooking", false);
            agent.destination = player.transform.position;

            // �ٽ� �÷��̾ �ٶ��� ������ ���� ��� �����ϵ��� ����
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
        
        isAttacking = true; // ���� �� ���� ����
        agent.isStopped = true; //���� �� �̵� ���߱�
        playerCam.SetActive(false);
        attackCam.SetActive(true);
        PlayerStats.Instance.TakeDamage(1);
        currentHP = PlayerStats.Instance.Health;
        yield return new WaitForSeconds(0.1f);

        shaderControllerScript.SetPlayerHealthSmoothly(0.2f, 3f);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(3f);

        

        isAttacking = false; 
        agent.isStopped = false; //���� �� �̵� ���߱�
        playerCam.SetActive(true);
        attackCam.SetActive(false);
        yeppy_spawn.spawned = false;
        Destroy(gameObject);
    }
}

