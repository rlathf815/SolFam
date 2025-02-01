using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ
    public float detectionRange = 10f; // ���� �Ÿ� ����
    public Light alertLight; // ��� ��
    public float detectionAngle = 60f; // ���� ���� ���� ���� �����Ͽ� ���� �� ���� ���� ��õ ����
    public float closeRangeDetection = 4f; // �ٰŸ� ���� �Ÿ� ����

    private NavMeshAgent agent;
    private bool isChasing = false; // ���� ���� üũ
    private Rigidbody rb;
    private Animator animator; // �ִϸ����� �߰�

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������

        agent.updateRotation = true; // NavMeshAgent�� �ڵ����� ȸ���ϵ��� ����
        agent.updatePosition = true;
        agent.avoidancePriority = 50; // �ٸ� ������Ʈ�� �浹 �� �켱���� ����
        agent.stoppingDistance = 2f; // �÷��̾ ������ ����
        agent.autoBraking = false; // ���� ���� ��� �̵�

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (alertLight == null)
        {
            alertLight = GetComponentInChildren<Light>();
        }

        // ���� �Ÿ� �� ������ ���� ������ �°� ����
        alertLight.range = detectionRange * 1.1f; // ���� �Ÿ����� �ణ �а� ����
        alertLight.spotAngle = detectionAngle; // ���� ���� ����

        alertLight.enabled = true; // ���� �׻� �ѵ� (���� �����̹Ƿ�)
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // ���� ���� ����: �÷��̾ 180�� ���ʿ� ������ ������ �������� ����
        if (!isChasing && distance <= detectionRange && angleToPlayer <= detectionAngle / 2 && angleToPlayer < 90f)
        {
            isChasing = true; // ���� ����
            alertLight.enabled = false; // �����Ǹ� �� ����
            Debug.Log("[EnemyAI] Player detected! Chasing started.");
        }

        if (isChasing)
        {
            agent.SetDestination(player.position); // �÷��̾� ����

            // ����� �ణ ���鵵 �����ǵ��� ���� ��ȭ (45�� ���ܿ��� ����)
            if (angleToPlayer <= 45f)
            {
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            }

            // Walking �ִϸ��̼� ����
            if (animator != null)
            {
                animator.SetBool("walking", true); // ���� ���� �� �ȱ� �ִϸ��̼� ����
                Debug.Log("Walking Animation Triggered: " + true); // ����� �α� �߰�
            }
        }
        else
        {
            // ���� ���� �ƴϸ� �ȱ� �ִϸ��̼� ���߱�
            if (animator != null)
            {
                animator.SetBool("walking", false); // �ȱ� ���߱�
                Debug.Log("Walking Animation Triggered: " + false); // ����� �α� �߰�
            }
        }
    }

    void FixedUpdate()
    {
        // �÷��̾ ������ �и��� �ʵ��� ������ �浹 ���� �� ���� ���� �߰�
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // �̵� �� �̲����� ����
            rb.angularVelocity = Vector3.zero;
            rb.freezeRotation = true; // ȸ�� ���
            rb.constraints = RigidbodyConstraints.FreezeAll; // ��� �̵� �� ȸ�� ���� (���� ����)
        }
    }
}

