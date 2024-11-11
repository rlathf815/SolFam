using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public Transform spotlight;
    public float detectionRadius = 10f;
    public float chaseSpeed = 5f;
    public float pathFollowDistance = 0.5f;
    public float recordInterval = 0.5f; // Interval for recording player position

    private bool isChasing = false;
    private List<Vector3> playerPath;
    private float lastRecordTime = 0f;

    private enum AIState { Idle, Chasing }
    private AIState currentState;

    void Start()
    {
        playerPath = new List<Vector3>();
        currentState = AIState.Idle;
        StartCoroutine(ToggleSpotlight());
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Chasing:
                FollowPlayerPath();
                LookAtPlayer();
                spotlight.gameObject.SetActive(true);
                RecordPlayerPosition();
                break;
            case AIState.Idle:
                DetectPlayer();
                LookAtMovingDirection(); // 이동 방향을 바라보도록 추가
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
                RecordPlayerPosition();
            }
        }
    }

    void FollowPlayerPath()
    {
        if (playerPath.Count == 0) return;

        Vector3 targetPosition = playerPath[0];
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // Smooth movement
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);

        // Remove the target if reached
        if (Vector3.Distance(transform.position, targetPosition) < pathFollowDistance)
        {
            playerPath.RemoveAt(0);
        }
    }

    void RecordPlayerPosition()
    {
        // Check if it's time to record the player's position
        if (Time.time - lastRecordTime >= recordInterval)
        {
            if (playerPath.Count == 0 || Vector3.Distance(playerPath[playerPath.Count - 1], player.position) > pathFollowDistance)
            {
                playerPath.Add(player.position);
                lastRecordTime = Time.time;
            }
        }
    }

    void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void LookAtMovingDirection()
    {
        if (playerPath.Count > 0)
        {
            Vector3 directionToNextPoint = (playerPath[0] - transform.position).normalized;
            if (directionToNextPoint != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToNextPoint);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
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
            if (currentState == AIState.Idle)
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

        // Draw the field of view
        Gizmos.color = Color.red;
        Vector3 leftBoundary = Quaternion.Euler(0, -45, 0) * transform.forward * detectionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, 45, 0) * transform.forward * detectionRadius;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
