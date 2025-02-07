using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Leguar.LowHealth;

public class Nam_yun : MonoBehaviour
{
    public LowHealthController shaderControllerScript;

    public GameObject player;
    private NavMeshAgent agent;
    private Animator anim;
    public static bool punch=false;
    private bool isAttacking=false;

    public GameObject playerCam;
    public GameObject attackCam;

    public AudioSource comeHere;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent =GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.isStopped = false;
        anim.SetBool("isWalk",true);
        comeHere.Play();
    }

    void Update()
    {
        if (punch)
        {
            punch = false;
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer()); 
            }
        }
        if (yun_ui.hasopenui == true&&yun_ui.openui==true)
        {
            agent.isStopped = true;
        }
        else if(yun_ui.openui == false&&yun_ui.hasopenui == true)
        {
            yun_ui.hasopenui = false;
            Destroy(gameObject);
        }
        else
        {
            agent.destination = player.transform.position;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player"&&yeppy_player.catched==false)
        {
            anim.SetBool("isWalk", false);
            yun_ui.openui = true;
            yeppy_player.catched = true;
            yeppy_player.gojung = player.transform.rotation;
            FirstPersonLook.canlook = false;
            Jump.jumpStrength = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            yeppy_player.catched = true;
            FirstPersonMovement.canRun = false;
            FirstPersonMovement.speed = 0f;
            Crouch.movementSpeed = 0f;
        }
    }
    private IEnumerator AttackPlayer()
    {

        isAttacking = true; // 공격 중 상태 설정
        agent.isStopped = true; //공격 중 이동 멈추기
        playerCam.SetActive(false);
        attackCam.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        shaderControllerScript.SetPlayerHealthSmoothly(0, 3f);
        anim.SetTrigger("Punch");

        yield return new WaitForSeconds(3f);
        PlayerStats.Instance.TakeDamage(3);

    }
}
