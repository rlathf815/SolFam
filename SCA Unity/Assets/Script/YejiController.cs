using DevionGames.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class YejiController : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogueCam;
    public GameObject playerCam;
    public LobbyTrigger lobbyTrigger;
    public GameObject dialogueUI; // 대화 UI
    public UnityEngine.UI.Text Name;
    public UnityEngine.UI.Text context;
    public float workTime;
    public Transform deskPosition; // 실장님 자리
    public Transform[] roamingPoints; // 실장님 순찰 루트

    private NavMeshAgent agent;
    private Animator animator;
    private bool isInteracting = false;
    private bool isRoaming = false; // 순찰 중인지 체크
    private bool isAtDesk = true; // 현재 자리에서 대기 중인지
    private Queue<string> dialogueQueue = new Queue<string>(); // 대사 저장

    private int playerMoney = 0;
    public ItemCollection playerInventory; // 액션바

    public AudioSource gun;
    public AudioSource money;
    public AudioSource interact;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 로비 감지 트리거 이벤트 연결
        if (lobbyTrigger != null)
        {
            lobbyTrigger.onPlayerEnter += OnPlayerEnterLobby;
        }

        // 실장님 처음에 자리에서 시작
        transform.position = deskPosition.position;
        animator.SetBool("isSitting", true);

        // 순찰 루틴 시작
        StartCoroutine(RoamingRoutine());
    }

    private void Update()
    {
        if (isInteracting && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    private IEnumerator RoamingRoutine()
    {
        while (true)
        {
            Debug.Log("==================로밍 시작 ======================");

            // 자리에서 20초 동안 대기
            animator.applyRootMotion = true;
            isAtDesk = true;
            agent.isStopped = true;
            animator.SetBool("isSitting", true);
            animator.SetBool("isWalking", false);
            //agent.isStopped = true;
            yield return new WaitForSeconds(workTime);

            // 자리에서 일어나서 순찰 시작
            isAtDesk = false;
            animator.SetBool("isSitting", false);
            animator.SetBool("isWalking", true);
            yield return new WaitForSeconds(2f);
            agent.isStopped = false;
            isRoaming = true;
            
            // 지정된 포인트를 따라 이동
            foreach (Transform point in roamingPoints)
            {          
                animator.applyRootMotion = false;
                agent.SetDestination(point.position);
                animator.SetBool("isWalking", true);
                if (lobbyTrigger.IsPlayerInLobby())
                {
                    Debug.Log("플레이어가 로비에 있어서 즉시 상호작용 시작!");
                    OnPlayerEnterLobby();
                    yield break; // 순찰 중단
                }
                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }

                animator.SetBool("isWalking", false);
                //yield return new WaitForSeconds(0.2f); // 각 지점에서 잠시 대기
            }

            // 순찰이 끝나면 자리로 돌아가기
            StartCoroutine(ReturnToDesk());
        }
    }

    private void OnPlayerEnterLobby()
    {
        if (isInteracting) return;
        if(isAtDesk) { return; }

        isInteracting = true;
        agent.isStopped = true;
        isRoaming = false;
        animator.SetBool("isWalking", false);
        animator.SetTrigger("interact");

        TeleportToPlayer();
        StartYejiDialogue();
    }

    private void TeleportToPlayer()
    {
        transform.position = player.transform.position + player.transform.forward * 1.5f;
        transform.LookAt(player.transform);

        dialogueCam.SetActive(true);
        playerCam.SetActive(false);
    }

    private void StartYejiDialogue()
    {
        dialogueQueue.Clear();
        dialogueQueue.Enqueue("학생, 결제하고 가야지~?");
        interact.Play();
        dialogueUI.SetActive(true);
        Name.text = "실장님";
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            ProcessPayment();
            return;
        }

        context.text = dialogueQueue.Dequeue();
    }

    private void ProcessPayment()
    {
        if (playerInventory != null && playerInventory.HasEnoughMoney(3000))
        {
            playerInventory.DeductMoney(3000);
            animator.SetTrigger("pay");
            money.Play();
            dialogueQueue.Enqueue("결제 완료됐어^^");
        }
        else
        {
            PlayerStats.Instance.TakeDamage(1);
            animator.SetTrigger("shoot");
            gun.Play();
            dialogueQueue.Enqueue("돈이 없잖아?!");
        }

        DisplayNextLine();
        StartCoroutine(EndInteraction());
    }

    private IEnumerator EndInteraction()
    {
        yield return new WaitForSeconds(2.3f);

        dialogueUI.SetActive(false);
        dialogueCam.SetActive(false);
        playerCam.SetActive(true);

        isInteracting = false;
        ReturnToDeskInstantly();
    }

    private IEnumerator ReturnToDesk()
    {
        isRoaming = false;
        agent.SetDestination(deskPosition.position);

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        transform.rotation = deskPosition.rotation;
        animator.SetBool("isWalking", false);
        animator.SetBool("isSitting", true);
        transform.rotation = deskPosition.rotation;
        agent.isStopped = true;
    }
    private void ReturnToDeskInstantly()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isSitting", true);
        isRoaming = false;

        // 즉시 자리로 순간이동
        transform.position = deskPosition.position;
        transform.rotation = deskPosition.rotation;

        // 에이전트 멈춤
        agent.isStopped = true;
        StartCoroutine(RoamingRoutine());
    }

}
