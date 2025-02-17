using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Nam_yun : MonoBehaviour
{
    public FastGlitch glitch;

    public GameObject player;
    private NavMeshAgent agent;
    private Animator anim;
    public static bool punch=false;
    private bool isAttacking=false;

    public GameObject playerCam;
    public GameObject attackCam;

    public AudioSource comeHere;
    public AudioSource choke;
    public AudioSource beam;



    void Start()
    {
        yeppy_player.catched = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");

        agent = GetComponent<NavMeshAgent>();
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
        if (col.CompareTag("Player") && !yeppy_player.catched)
        {
            anim.SetBool("isWalk", false);
            yun_ui.openui = true;
            yeppy_player.catched = true; // 중복 방지

            // 기존 상태 저장
            yeppy_player.gojung = player.transform.rotation;

            // 플레이어 조작 불가
            if (FirstPersonLook.canlook)
            {
                FirstPersonLook.canlook = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; // 한 번만 설정
            }

            Jump.jumpStrength = 0;
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

        glitch.PixelGlitch = 1f;
        glitch.FrameGlitch = 1f;
        glitch.ChromaticGlitch = 1f;

        anim.SetTrigger("Punch");
        yield return new WaitForSeconds(0.1f);
        beam.Play();
        yield return new WaitForSeconds(1.5f);
        choke.Play();
        yield return new WaitForSeconds(3f);

        PlayerStats.Instance.TakeDamage(3);

    }
}
