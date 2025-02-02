using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Leguar.LowHealth;

public class EnemyAI : MonoBehaviour
{
    public LowHealthController shaderControllerScript;

    public Transform player; // �÷��̾� ������Ʈ
    public float detectionRange = 10f; // ���� �Ÿ�
    public Light alertLight; // ��� ��
    public float detectionAngle = 60f; // ���� ����
    public float closeRangeDetection = 4f; // �ٰŸ� ���� �Ÿ�
    public float attackRange = 1.5f;
    public float moveRadius = 10f; // ���� �̵� ����
    public float waitTime = 2f; // ���� �̵� ���� �� ��� �ð�

    private Transform activeCoffeeTarget;
    private NavMeshAgent agent;
    private bool isChasing = false;
    private bool isRoaming = true; // ���� �̵� Ȱ��ȭ
    private bool isAttacking = false;
    private Rigidbody rb;
    private Animator animator;
    public GameObject CoffeeInHand;
    public GameObject playerCam;
    public GameObject attackCam;

    private Vector3 startPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.avoidancePriority = 50;
        agent.autoBraking = false;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (alertLight == null)
        {
            alertLight = GetComponentInChildren<Light>();
        }

        alertLight.range = detectionRange;
        alertLight.spotAngle = detectionAngle;
        alertLight.enabled = true;

        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(false); // ���� ���� �� ��Ȱ��ȭ
        }
        playerCam.SetActive(true);
        attackCam.SetActive(false);
        startPosition = transform.position; // ���� ��ġ ����
        StartCoroutine(MoveRandomly()); // ���� �̵� ����
    }

    void Update()
    {
        if (isAttacking) return;

        DetectCoffee(); // Ŀ�� ����
        if (activeCoffeeTarget != null)
        {
            ChaseCoffee(); // Ŀ�� ����
        }
        else
        {
            DetectPlayer(); // �÷��̾� ����
            if (isChasing)
            {
                ChasePlayer();
                //agent.SetDestination(player.position); // �÷��̾� ����
            }
        }
    }

    void DetectCoffee()
    {
        if (activeCoffeeTarget == null)
        {
            GameObject coffee = GameObject.FindGameObjectWithTag("activeCoffee");
            if (coffee != null)
            {
                activeCoffeeTarget = coffee.transform;
            }
        }
    }

    void ChaseCoffee()
    {
        float coffeeDistance = Vector3.Distance(transform.position, activeCoffeeTarget.position);
        agent.stoppingDistance = 1f;

        if (coffeeDistance <= detectionRange)
        {
            isChasing = true;
            isRoaming = false; // ���� �̵� ����
            animator.SetBool("isChasing", true);
            alertLight.enabled = false;
            agent.SetDestination(activeCoffeeTarget.position);
        }

        if (coffeeDistance <= agent.stoppingDistance)
        {
            Destroy(activeCoffeeTarget.gameObject);
            activeCoffeeTarget = null;
            isChasing = false;
            isRoaming = false;
            animator.SetBool("isChasing", false);
            alertLight.enabled = true;
            StartCoroutine(HandleCoffeeInteraction());

        }
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

        if (dotProduct > Mathf.Cos(detectionAngle * Mathf.Deg2Rad) && distance <= detectionRange)
        {
            if (distance <= closeRangeDetection)
            {
                isChasing = true;
                isRoaming = false;
                animator.SetBool("isChasing", true);
                alertLight.enabled = false;
                agent.stoppingDistance = 2f;
                agent.SetDestination(player.position);
            }
            else if (!isChasing && distance <= detectionRange)
            {
                isChasing = true;
                isRoaming = false;
                animator.SetBool("isChasing", true);
                alertLight.enabled = false;
                agent.stoppingDistance = 2f;
                agent.SetDestination(player.position);

            }
        }
    }
    void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.SetDestination(player.position); //  ���� �Ÿ� �̻��̸� ��� ����
        }
        else
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer()); // ���� �Ÿ� ���� ������ ���� ����
            }
        }
    }
    IEnumerator MoveRandomly()
    {
        while (true)
        {
            if (!isChasing) // �÷��̾ ���� ���� �ƴ� ���� ���� �̵�
            {
                isRoaming = true;
                Vector3 randomDestination = GetRandomPoint(startPosition, moveRadius);
                agent.SetDestination(randomDestination);

                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                isRoaming = false;
            }
            yield return null;
        }
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            randomPos.y = center.y;

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center;
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }



    private IEnumerator HandleCoffeeInteraction()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(3f);
        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(true);
        }

        yield return new WaitForSeconds(11f);

        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(false);
        }
        agent.isStopped = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.other.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackPlayer());
        }
    }
    private IEnumerator AttackPlayer()
    {

        isAttacking = true; // ���� �� ���� ����
        agent.isStopped = true; //���� �� �̵� ���߱�
        playerCam.SetActive(false);
        attackCam.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        shaderControllerScript.SetPlayerHealthSmoothly(0, 3f);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(3f);
        PlayerStats.Instance.TakeDamage(3);
    }
}
