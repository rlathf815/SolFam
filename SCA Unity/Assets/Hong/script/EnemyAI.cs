using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public float detectionRange = 10f; // 감지 거리
    public Light alertLight; // 경고 빛
    public float detectionAngle = 60f; // 감지 각도
    public float closeRangeDetection = 4f; // 근거리 감지 거리
    private Transform activeCoffeeTarget; // activeCoffee 태그를 가진 오브젝트

    private NavMeshAgent agent;
    private bool isChasing = false; // 감지 여부 체크
    private Rigidbody rb;
    private Animator animator; // 애니메이터 추가
    public GameObject CoffeeInHand;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기

        agent.updateRotation = true; // NavMeshAgent가 자동으로 회전하도록 설정
        agent.updatePosition = true;
        agent.avoidancePriority = 50; // 다른 오브젝트와 충돌 시 우선순위 조정
        agent.autoBraking = false; // 감속 없이 계속 이동

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (alertLight == null)
        {
            alertLight = GetComponentInChildren<Light>();
        }

        // 감지 거리 및 각도를 빛의 범위에 맞게 조정
        alertLight.range = detectionRange; // 경고 빛의 범위를 감지 범위와 동일하게 설정
        alertLight.spotAngle = detectionAngle; // 감지 각도 조정

        alertLight.enabled = true; // 빛을 항상 켜둠 (감지 기준이므로)
        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(false); // 게임 시작 시 비활성화
        }
    }

    void Update()
    {
        // activeCoffee 태그를 가진 오브젝트가 있다면 그걸 우선 추적
        if (activeCoffeeTarget == null)
        {
            GameObject coffee = GameObject.FindGameObjectWithTag("activeCoffee");
            if (coffee != null)
            {
                activeCoffeeTarget = coffee.transform; // activeCoffee가 있으면 그걸 추적
            }
        }

        // activeCoffee를 추적할 때
        if (activeCoffeeTarget != null)
        {
            float coffeeDistance = Vector3.Distance(transform.position, activeCoffeeTarget.position);
            agent.stoppingDistance = 0f; // activeCoffee 추적 시 stoppingDistance를 0으로 설정 (거리를 두지 않음)

            if (coffeeDistance <= detectionRange)
            {
                isChasing = true; // activeCoffee를 추적 중
                animator.SetBool("isChasing", true); // 애니메이션 변경
                alertLight.enabled = false; // 빛 끄기
                agent.SetDestination(activeCoffeeTarget.position); // activeCoffee로 이동
            }

            // activeCoffee에 도달하면, 원래 상태로 돌아가기
            if (coffeeDistance <= agent.stoppingDistance)
            {
                Destroy(activeCoffeeTarget.gameObject); // activeCoffee 오브젝트 삭제
                activeCoffeeTarget = null; // activeCoffeeTarget을 null로 설정
                isChasing = false; // 추적 종료
                animator.SetBool("isChasing", false); // 애니메이션 변경
                alertLight.enabled = true; // 경고 빛 다시 켜기
            }
        }
        else
        {
            // 플레이어 감지 로직
            float distance = Vector3.Distance(transform.position, player.position);
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // 전방에서 플레이어를 감지하려면 Dot product를 사용
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            // 플레이어가 정면 범위 내에 있을 경우만 감지 (Dot product가 양수일 때만 정면)
            if (dotProduct > Mathf.Cos(detectionAngle * Mathf.Deg2Rad) && distance <= detectionRange)
            {
                if (distance <= closeRangeDetection)
                {
                    isChasing = true;
                    animator.SetBool("isChasing", true); // 애니메이션 변경
                    alertLight.enabled = false; // 감지되면 빛 끄기
                    agent.stoppingDistance = 2f; // 플레이어를 추적할 때 멈추는 거리 2f로 설정
                    agent.SetDestination(player.position); // 플레이어 추적
                }
                else if (!isChasing && distance <= detectionRange)
                {
                    isChasing = true; // 감지 시작
                    animator.SetBool("isChasing", true); // 애니메이션 변경
                    alertLight.enabled = false; // 감지되면 빛 끄기
                    agent.stoppingDistance = 2f; // 플레이어를 감지했을 때 stoppingDistance를 2로 설정
                    agent.SetDestination(player.position); // 플레이어 추적
                }
            }

            // 플레이어 추적
            if (isChasing)
            {
                agent.SetDestination(player.position); // 플레이어 추적
            }
        }
    }

    void FixedUpdate()
    {
        // 플레이어가 몹에게 밀리지 않도록 물리적 충돌 제거 및 정밀 제어 추가
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // 이동 중 미끄러짐 방지
            rb.angularVelocity = Vector3.zero;
            rb.freezeRotation = true; // 회전 잠금
            rb.constraints = RigidbodyConstraints.FreezeAll; // 모든 이동 및 회전 방지 (완전 고정)
        }
    }

    // activeCoffee 태그를 가진 오브젝트와 충돌했을 때 해당 오브젝트 삭제
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("activeCoffee"))
        {
            Destroy(collision.gameObject); // activeCoffee 오브젝트 삭제
            activeCoffeeTarget = null; // activeCoffee 추적을 멈춤
            isChasing = false; // 추적 종료
            animator.SetBool("isChasing", false); // 애니메이션 변경
            alertLight.enabled = true; // 경고 빛 다시 켜기
            StartCoroutine(HandleCoffeeInteraction());
        }
    }
    private IEnumerator HandleCoffeeInteraction()
    {
        yield return new WaitForSeconds(3f);
        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(true); // 커피를 든 상태로 변경
        }

        yield return new WaitForSeconds(10f); // 5초 동안 유지

        if (CoffeeInHand != null)
        {
            CoffeeInHand.SetActive(false); // 다시 비활성화
        }

    }
}
