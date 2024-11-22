using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public Transform spotlight;
    public float detectionRadius1 = 10f; // 감지 범위 1
    public float detectionRadius2 = 30f; // 감지 범위 2
    public float detectionRadius3 = 15f; // 감지 범위 3
    public float chaseSpeed = 5f;        // 추격 속도
    public float rotationSpeed = 5f;    // 회전 속도
    public float lostPlayerDelay = 3f;  // 플레이어를 잃었을 때 대기 시간
    public float positionThreshold = 0.5f; // 마지막 위치에 도달했다고 간주하는 거리

    public float viewAngle = 45f; // **시야각**: 플레이어를 감지할 수 있는 각도

    private float timeSinceLastSeen = 0f; // 마지막으로 플레이어를 본 시간
    private Vector3 lastKnownPosition;   // 마지막으로 감지된 플레이어 위치
    private bool isPlayerInSight = false; // 플레이어가 현재 시야에 있는지 확인
    private List<Vector3> playerPath = new List<Vector3>(); // 플레이어 경로 저장
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
                if (IsPlayerInFieldOfView() && IsPlayerWithinRange(detectionRadius1))
                {
                    // 범위 1 내에서 플레이어 추격
                    isPlayerInSight = true;
                    timeSinceLastSeen = 0f;
                    lastKnownPosition = player.position;
                    ChasePlayer(player.position);
                    RotateTowards(player.position - transform.position);
                }
                else if (IsPlayerWithinRange(detectionRadius3))
                {
                    // 범위 3 내에서 플레이어 추격 (실시간 추적)
                    isPlayerInSight = false;
                    playerPath.Clear(); // 기존 경로 초기화
                    ChasePlayer(player.position);
                    RotateTowards(player.position - transform.position);
                }
                else if (IsPlayerWithinRange(detectionRadius2))
                {
                    // 범위 2 내에서 이동 경로를 따라 이동
                    isPlayerInSight = false;
                    SavePlayerPath(); // 플레이어 경로 저장
                    FollowPlayerPath();
                }
                else
                {
                    // 범위 2 및 3에서 벗어난 경우
                    timeSinceLastSeen += Time.deltaTime;

                    if (timeSinceLastSeen > lostPlayerDelay)
                    {
                        currentState = AIState.Idle;
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

            if (distanceToPlayer <= detectionRadius1)
            {
                currentState = AIState.Chasing;
                timeSinceLastSeen = 0f;
                lastKnownPosition = player.position;
            }
        }
    }

    void SavePlayerPath()
    {
        // 플레이어의 현재 위치를 경로에 저장
        if (playerPath.Count == 0 || Vector3.Distance(player.position, playerPath[playerPath.Count - 1]) > positionThreshold)
        {
            playerPath.Add(player.position);
        }
    }

    void FollowPlayerPath()
    {
        // 경로 따라 이동
        if (playerPath.Count > 0)
        {
            Vector3 targetPosition = playerPath[0];
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget <= positionThreshold)
            {
                // 경로의 현재 지점에 도달하면 제거
                playerPath.RemoveAt(0);
            }
            else
            {
                // 경로 따라 이동
                MoveTowards(targetPosition);
                RotateTowards(targetPosition - transform.position);
            }
        }
    }

    void ChasePlayer(Vector3 targetPosition)
    {
        MoveTowards(targetPosition);
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        transform.position += directionToTarget * chaseSpeed * Time.deltaTime;
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

    bool IsPlayerWithinRange(float range)
    {
        return Vector3.Distance(transform.position, player.position) <= range;
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
        Gizmos.DrawWireSphere(transform.position, detectionRadius1);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius2);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius3);

        Gizmos.color = Color.red;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle, 0) * transform.forward * detectionRadius1;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle, 0) * transform.forward * detectionRadius1;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
