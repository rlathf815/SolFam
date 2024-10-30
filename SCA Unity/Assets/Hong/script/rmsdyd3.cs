using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour
{
    public Transform player;              // 플레이어의 Transform
    public Transform spotlight;           // 스포트라이트의 Transform
    public float detectionRadius = 10f;   // 감지 반경
    public float chaseSpeed = 5f;         // 추적 속도
    public float pathFollowDistance = 0.5f; // 경로 따라가기 거리

    private bool isChasing = false;       // 추적 중인지 여부
    private List<Vector3> playerPath;     // 플레이어 경로 저장

    void Start()
    {
        playerPath = new List<Vector3>();
        StartCoroutine(ToggleSpotlight());
    }

    void Update()
    {
        if (isChasing)
        {
            FollowPlayerPath();
            LookAtPlayer();
            spotlight.gameObject.SetActive(true);
            RecordPlayerPosition(); // 플레이어 위치를 계속 기록
        }
        else
        {
            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        if (spotlight.gameObject.activeSelf && IsPlayerInFieldOfView())
        {
            float distanceToPlayer = Vector3.Distance(spotlight.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                isChasing = true; // 플레이어를 감지하면 추적 시작
                RecordPlayerPosition(); // 감지 후 플레이어 위치 기록 시작
            }
        }
    }

    void FollowPlayerPath()
    {
        if (playerPath.Count == 0) return;

        Vector3 targetPosition = playerPath[0];
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // 이동
        if (directionToTarget != Vector3.zero)
        {
            transform.position += directionToTarget * chaseSpeed * Time.deltaTime;
        }

        // 목표 위치에 도달하면 경로에서 제거
        if (Vector3.Distance(transform.position, targetPosition) < pathFollowDistance)
        {
            playerPath.RemoveAt(0);
        }
    }

    void RecordPlayerPosition()
    {
        // 플레이어 위치를 기록
        if (playerPath.Count == 0 || Vector3.Distance(playerPath[playerPath.Count - 1], player.position) > pathFollowDistance)
        {
            playerPath.Add(player.position);
        }
    }

    void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < 45f;
    }

    System.Collections.IEnumerator ToggleSpotlight()
    {
        while (true)
        {
            if (!isChasing)
            {
                spotlight.gameObject.SetActive(!spotlight.gameObject.activeSelf);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return null;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        foreach (Vector3 point in playerPath)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
