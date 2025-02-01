using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public float detectionRange = 10f; // 감지 거리 증가
    public Light alertLight; // 경고 빛
    public float detectionAngle = 60f; // 감지 각도 더욱 좁게 설정하여 측면 및 뒤쪽 감지 원천 차단
    public float closeRangeDetection = 4f; // 근거리 감지 거리 증가

    private NavMeshAgent agent;
    private bool isChasing = false; // 감지 여부 체크
    private Rigidbody rb;
    private Animator animator; // 애니메이터 추가

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기

        agent.updateRotation = true; // NavMeshAgent가 자동으로 회전하도록 설정
        agent.updatePosition = true;
        agent.avoidancePriority = 50; // 다른 오브젝트와 충돌 시 우선순위 조정
        agent.stoppingDistance = 2f; // 플레이어를 끝까지 추적
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
        alertLight.range = detectionRange * 1.1f; // 감지 거리보다 약간 넓게 조정
        alertLight.spotAngle = detectionAngle; // 감지 각도 조정

        alertLight.enabled = true; // 빛을 항상 켜둠 (감지 기준이므로)
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // 뒤쪽 감지 차단: 플레이어가 180도 뒤쪽에 있으면 추적을 시작하지 않음
        if (!isChasing && distance <= detectionRange && angleToPlayer <= detectionAngle / 2 && angleToPlayer < 90f)
        {
            isChasing = true; // 감지 시작
            alertLight.enabled = false; // 감지되면 빛 끄기
            Debug.Log("[EnemyAI] Player detected! Chasing started.");
        }

        if (isChasing)
        {
            agent.SetDestination(player.position); // 플레이어 추적

            // 정면과 약간 측면도 감지되도록 각도 완화 (45도 내외에서 감지)
            if (angleToPlayer <= 45f)
            {
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            }

            // Walking 애니메이션 제어
            if (animator != null)
            {
                animator.SetBool("walking", true); // 추적 중일 때 걷기 애니메이션 실행
                Debug.Log("Walking Animation Triggered: " + true); // 디버그 로그 추가
            }
        }
        else
        {
            // 추적 중이 아니면 걷기 애니메이션 멈추기
            if (animator != null)
            {
                animator.SetBool("walking", false); // 걷기 멈추기
                Debug.Log("Walking Animation Triggered: " + false); // 디버그 로그 추가
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
}

