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
    public GameObject dialogueUI; // ��ȭ UI
    public UnityEngine.UI.Text Name;
    public UnityEngine.UI.Text context;
    public float workTime;
    public Transform deskPosition; // ����� �ڸ�
    public Transform[] roamingPoints; // ����� ���� ��Ʈ

    private NavMeshAgent agent;
    private Animator animator;
    private bool isInteracting = false;
    private bool isRoaming = false; // ���� ������ üũ
    private bool isAtDesk = true; // ���� �ڸ����� ��� ������
    private Queue<string> dialogueQueue = new Queue<string>(); // ��� ����

    private int playerMoney = 0;
    public ItemCollection playerInventory; // �׼ǹ�

    public AudioSource gun;
    public AudioSource money;
    public AudioSource interact;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // �κ� ���� Ʈ���� �̺�Ʈ ����
        if (lobbyTrigger != null)
        {
            lobbyTrigger.onPlayerEnter += OnPlayerEnterLobby;
        }

        // ����� ó���� �ڸ����� ����
        transform.position = deskPosition.position;
        animator.SetBool("isSitting", true);

        // ���� ��ƾ ����
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
            Debug.Log("==================�ι� ���� ======================");

            // �ڸ����� 20�� ���� ���
            animator.applyRootMotion = true;
            isAtDesk = true;
            agent.isStopped = true;
            animator.SetBool("isSitting", true);
            animator.SetBool("isWalking", false);
            //agent.isStopped = true;
            yield return new WaitForSeconds(workTime);

            // �ڸ����� �Ͼ�� ���� ����
            isAtDesk = false;
            animator.SetBool("isSitting", false);
            animator.SetBool("isWalking", true);
            yield return new WaitForSeconds(2f);
            agent.isStopped = false;
            isRoaming = true;
            
            // ������ ����Ʈ�� ���� �̵�
            foreach (Transform point in roamingPoints)
            {          
                animator.applyRootMotion = false;
                agent.SetDestination(point.position);
                animator.SetBool("isWalking", true);
                if (lobbyTrigger.IsPlayerInLobby())
                {
                    Debug.Log("�÷��̾ �κ� �־ ��� ��ȣ�ۿ� ����!");
                    OnPlayerEnterLobby();
                    yield break; // ���� �ߴ�
                }
                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }

                animator.SetBool("isWalking", false);
                //yield return new WaitForSeconds(0.2f); // �� �������� ��� ���
            }

            // ������ ������ �ڸ��� ���ư���
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
        dialogueQueue.Enqueue("�л�, �����ϰ� ������~?");
        interact.Play();
        dialogueUI.SetActive(true);
        Name.text = "�����";
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
            dialogueQueue.Enqueue("���� �Ϸ�ƾ�^^");
        }
        else
        {
            PlayerStats.Instance.TakeDamage(1);
            animator.SetTrigger("shoot");
            gun.Play();
            dialogueQueue.Enqueue("���� ���ݾ�?!");
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

        // ��� �ڸ��� �����̵�
        transform.position = deskPosition.position;
        transform.rotation = deskPosition.rotation;

        // ������Ʈ ����
        agent.isStopped = true;
        StartCoroutine(RoamingRoutine());
    }

}
