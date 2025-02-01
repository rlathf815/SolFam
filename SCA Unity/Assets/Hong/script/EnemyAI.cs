using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ
    public float detectionRange = 10f; // ���� �Ÿ�
    public Light alertLight; // ��� ��
    public float detectionAngle = 60f; // ���� ����
    public float closeRangeDetection = 4f; // �ٰŸ� ���� �Ÿ�
    private Transform activeCoffeeTarget; // activeCoffee �±׸� ���� ������Ʈ

    private NavMeshAgent agent;
    private bool isChasing = false; // ���� ���� üũ
    private Rigidbody rb;
    private Animator animator; // �ִϸ����� �߰�
    public GameObject CoffeeInHand;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������

        agent.updateRotation = true; // NavMeshAgent�� �ڵ����� ȸ���ϵ��� ����
        agent.updatePosition = true;
        agent.avoidancePriority = 50; // �ٸ� ������Ʈ�� �浹 �� �켱���� ����
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
        alertLight.range = detectionRange; // ��� ���� ������ ���� ������ �����ϰ� ����
        alertLight.spotAngle = detectionAngle; // ���� ���� ����

        alertLight.enabled = true; // ���� �׻� �ѵ� (���� �����̹Ƿ�)
        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(false); // ���� ���� �� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        // activeCoffee �±׸� ���� ������Ʈ�� �ִٸ� �װ� �켱 ����
        if (activeCoffeeTarget == null)
        {
            GameObject coffee = GameObject.FindGameObjectWithTag("activeCoffee");
            if (coffee != null)
            {
                activeCoffeeTarget = coffee.transform; // activeCoffee�� ������ �װ� ����
            }
        }

        // activeCoffee�� ������ ��
        if (activeCoffeeTarget != null)
        {
            float coffeeDistance = Vector3.Distance(transform.position, activeCoffeeTarget.position);
            agent.stoppingDistance = 0f; // activeCoffee ���� �� stoppingDistance�� 0���� ���� (�Ÿ��� ���� ����)

            if (coffeeDistance <= detectionRange)
            {
                isChasing = true; // activeCoffee�� ���� ��
                animator.SetBool("isChasing", true); // �ִϸ��̼� ����
                alertLight.enabled = false; // �� ����
                agent.SetDestination(activeCoffeeTarget.position); // activeCoffee�� �̵�
            }

            // activeCoffee�� �����ϸ�, ���� ���·� ���ư���
            if (coffeeDistance <= agent.stoppingDistance)
            {
                Destroy(activeCoffeeTarget.gameObject); // activeCoffee ������Ʈ ����
                activeCoffeeTarget = null; // activeCoffeeTarget�� null�� ����
                isChasing = false; // ���� ����
                animator.SetBool("isChasing", false); // �ִϸ��̼� ����
                alertLight.enabled = true; // ��� �� �ٽ� �ѱ�
            }
        }
        else
        {
            // �÷��̾� ���� ����
            float distance = Vector3.Distance(transform.position, player.position);
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // ���濡�� �÷��̾ �����Ϸ��� Dot product�� ���
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            // �÷��̾ ���� ���� ���� ���� ��츸 ���� (Dot product�� ����� ���� ����)
            if (dotProduct > Mathf.Cos(detectionAngle * Mathf.Deg2Rad) && distance <= detectionRange)
            {
                if (distance <= closeRangeDetection)
                {
                    isChasing = true;
                    animator.SetBool("isChasing", true); // �ִϸ��̼� ����
                    alertLight.enabled = false; // �����Ǹ� �� ����
                    agent.stoppingDistance = 2f; // �÷��̾ ������ �� ���ߴ� �Ÿ� 2f�� ����
                    agent.SetDestination(player.position); // �÷��̾� ����
                }
                else if (!isChasing && distance <= detectionRange)
                {
                    isChasing = true; // ���� ����
                    animator.SetBool("isChasing", true); // �ִϸ��̼� ����
                    alertLight.enabled = false; // �����Ǹ� �� ����
                    agent.stoppingDistance = 2f; // �÷��̾ �������� �� stoppingDistance�� 2�� ����
                    agent.SetDestination(player.position); // �÷��̾� ����
                }
            }

            // �÷��̾� ����
            if (isChasing)
            {
                agent.SetDestination(player.position); // �÷��̾� ����
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

    // activeCoffee �±׸� ���� ������Ʈ�� �浹���� �� �ش� ������Ʈ ����
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("activeCoffee"))
        {
            Destroy(collision.gameObject); // activeCoffee ������Ʈ ����
            activeCoffeeTarget = null; // activeCoffee ������ ����
            isChasing = false; // ���� ����
            animator.SetBool("isChasing", false); // �ִϸ��̼� ����
            alertLight.enabled = true; // ��� �� �ٽ� �ѱ�
            StartCoroutine(HandleCoffeeInteraction());
        }
    }
    private IEnumerator HandleCoffeeInteraction()
    {
        yield return new WaitForSeconds(3f);
        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(true); // Ŀ�Ǹ� �� ���·� ����
        }

        yield return new WaitForSeconds(10f); // 5�� ���� ����

        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(false); // �ٽ� ��Ȱ��ȭ
        }

    }
}
