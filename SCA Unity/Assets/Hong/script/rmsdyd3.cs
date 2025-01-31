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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        agent.updateRotation = true; // NavMeshAgent�� �ڵ����� ȸ���ϵ��� ����
        agent.updatePosition = true;
        agent.avoidancePriority = 50; // �ٸ� ������Ʈ�� �浹 �� �켱���� ����
        agent.stoppingDistance = 0f; // �÷��̾ ������ ����
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

        // ���� �� ���� ���� ���� ���� (���� ���鿡 �ִ� �÷��̾ ����)
        if (!isChasing && (distance <= closeRangeDetection || (distance <= detectionRange && angleToPlayer <= detectionAngle / 2 && Vector3.Dot(transform.forward, directionToPlayer) > 0.999f)))
        {
            isChasing = true; // ���� ����
            alertLight.enabled = false; // �����Ǹ� �� ����
            Debug.Log("[EnemyAI] Player detected! Chasing started.");
        }

        if (isChasing)
        {
            agent.SetDestination(player.position); // �÷��̾� ����
            if (Vector3.Dot(transform.forward, directionToPlayer) > 0.9f) // ������ ���鿡 �ִ� ��쿡�� ȸ��
            {
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
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


