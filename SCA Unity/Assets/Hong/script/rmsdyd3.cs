using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public Transform spotlight;
    public float detectionRadius = 10f;
    public float chaseSpeed = 5f;
    public float obstacleDetectionDistance = 2f;
    public float viewAngle = 45f;
    public float rotationSpeed = 5f;
    public float lostPlayerDelay = 3f; // 플레이어를 잃은 후 Idle로 전환되는 시간

    private float timeSinceLastSeen = 0f; // 마지막으로 플레이어를 본 시간
    private Vector3 lastKnownPosition;   // 마지막으로 감지된 플레이어 위치
    private bool isPlayerInSight = false; // 플레이어가 현재 시야에 있는지 확인
    private enum AIState { Idle, Chasing }
    private AIState currentState;

    void Start()
    {
        currentState = AIState.Idle;
        StartCoroutine(ToggleSpotlight());
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Chasing:
                if (IsPlayerInFieldOfView() && IsPlayerWithinRange())
                {
                    isPlayerInSight = true;
                    timeSinceLastSeen = 0f;
                    lastKnownPosition = player.position; // 플레이어 위치 갱신
                    ChasePlayer(player.position);
                }
                else
                {
                    isPlayerInSight = false;
                    timeSinceLastSeen += Time.deltaTime;

                    if (timeSinceLastSeen > lostPlayerDelay)
                    {
                        // 일정 시간이 지나면 Idle 상태로 복귀
                        currentState = AIState.Idle;
                    }
                    else
                    {
                        // 플레이어가 보이지 않으면 마지막 위치를 추적
                        ChasePlayer(lastKnownPosition);
                    }
                }
                spotlight.gameObject.SetActive(true); // 추격 중에는 항상 켜기
                break;

            case AIState.Idle:
                DetectPlayer();
                break;
        }
    }

    void DetectPlayer()
    {
        if (spotlight.gameObject.activeSelf && IsPlayerInFieldOfView())
        {
            float distanceToPlayer = Vector3.Distance(spotlight.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                currentState = AIState.Chasing;
                timeSinceLastSeen = 0f; // 추적 시작 시 초기화
                lastKnownPosition = player.position; // 플레이어의 현재 위치 저장
            }
        }
    }

    void ChasePlayer(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // 벽 감지 및 회피 로직
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, obstacleDetectionDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Vector3 avoidDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
                directionToTarget = (directionToTarget + avoidDirection).normalized;
            }
        }

        // 이동
        transform.position += directionToTarget * chaseSpeed * Time.deltaTime;

        // 이동 방향으로 시선 회전
        RotateTowards(directionToTarget);
    }

    void RotateTowards(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < viewAngle;
    }

    bool IsPlayerWithinRange()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRadius;
    }

    System.Collections.IEnumerator ToggleSpotlight()
    {
        while (true)
        {
            if (currentState == AIState.Idle)
            {
                spotlight.gameObject.SetActive(!spotlight.gameObject.activeSelf);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                spotlight.gameObject.SetActive(true); // 추격 중일 때는 항상 켜기
                yield return null;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // 시야각 그리기
        Gizmos.color = Color.red;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle, 0) * transform.forward * detectionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle, 0) * transform.forward * detectionRadius;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // 장애물 감지 거리 표시
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * obstacleDetectionDistance);
    }
}
