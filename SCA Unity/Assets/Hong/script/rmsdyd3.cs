using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public Transform spotlight;

    public float detectionRadius1 = 10f;
    public float detectionRadius2 = 30f;
    public float detectionRadius3 = 20f;

    
    public float chaseSpeed = 4.5f;
    public float rotationSpeed = 5f;
    public float lostPlayerDelay = 3f;
    public float positionThreshold = 0.5f;
    public float viewAngle = 45f;
    public float obstacleAvoidanceDistance = 2f;

    
    private float timeSinceLastSeen = 0f;
    private Vector3 lastKnownPosition;
    private List<Vector3> playerPath = new List<Vector3>();
    private bool isPlayerInSight = false;

    private enum AIState { Idle, Chasing }
    private AIState currentState;

    private Rigidbody rb;

    void Start()
    {
        currentState = AIState.Idle;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody�� �������� �ʾҽ��ϴ�! Rigidbody�� �߰��ϼ���.");
        }

        StartCoroutine(ToggleSpotlight());
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Chasing:
                HandleChasingState();
                break;

            case AIState.Idle:
                DetectPlayer();
                break;
        }
    }

    void HandleChasingState()
    {
        if (IsPlayerInFieldOfView() && IsPlayerWithinRange(detectionRadius1))
        {
            Debug.Log("���� 1: �÷��̾� ���� ��");
            isPlayerInSight = true;
            timeSinceLastSeen = 0f;
            lastKnownPosition = player.position;
            MoveTowards(player.position);
            RotateTowards(player.position - transform.position); 
        }
        else if (IsPlayerWithinRange(detectionRadius3))
        {
            Debug.Log("���� 3: �÷��̾� �ǽð� ���� ��");
            isPlayerInSight = false;
            playerPath.Clear();
            lastKnownPosition = player.position;
            MoveTowards(player.position);
            RotateTowards(player.position - transform.position); 
        }
        else if (IsPlayerWithinRange(detectionRadius2))
        {
            Debug.Log("���� 2: ��� ���� �̵� ��");
            isPlayerInSight = false;
            SavePlayerPath();
            FollowPlayerPath();
            RotateTowards(player.position - transform.position); 
        }
        else
        {
            Debug.Log("���� 2 �� 3 ���: ������ ��ġ�� �̵�");
            timeSinceLastSeen += Time.deltaTime;

            if (timeSinceLastSeen > lostPlayerDelay)
            {
                Debug.Log("Idle ���·� ��ȯ");
                currentState = AIState.Idle;
            }
            else
            {
                MoveTowards(lastKnownPosition);
                RotateTowards(lastKnownPosition - transform.position); 
            }
        }

        spotlight.gameObject.SetActive(true);
    }

    void DetectPlayer()
    {
        if (spotlight.gameObject.activeSelf && IsPlayerInFieldOfView())
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius1)
            {
                Debug.Log("�÷��̾� ����: ���� ����");
                currentState = AIState.Chasing;
                timeSinceLastSeen = 0f;
                lastKnownPosition = player.position;
            }
        }
    }

    void SavePlayerPath()
    {
        if (playerPath.Count == 0 || Vector3.Distance(player.position, playerPath[playerPath.Count - 1]) > positionThreshold)
        {
            Debug.Log("��� ����: " + player.position);
            playerPath.Add(player.position);
        }
    }

    void FollowPlayerPath()
    {
        if (playerPath.Count > 0)
        {
            Vector3 targetPosition = playerPath[0];
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget <= positionThreshold)
            {
                Debug.Log("��� ���� ����: " + targetPosition);
                playerPath.RemoveAt(0);
            }
            else
            {
                Debug.Log("��� ���� �̵� ��: " + targetPosition);
                MoveTowards(targetPosition);
            }
        }
        else
        {
            Debug.Log("��� ����: ������ ��ġ�� �̵�");
            MoveTowards(lastKnownPosition);
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, obstacleAvoidanceDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("��ֹ� ����: ȸ�� ��");
                Vector3 avoidDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
                directionToTarget = (directionToTarget + avoidDirection).normalized;
            }
        }

        rb.velocity = directionToTarget * chaseSpeed;

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
                spotlight.gameObject.SetActive(true);
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

        Gizmos.color = Color.blue;
        for (int i = 0; i < playerPath.Count - 1; i++)
        {
            Gizmos.DrawLine(playerPath[i], playerPath[i + 1]);
        }

        if (playerPath.Count > 0)
        {
            Gizmos.DrawLine(transform.position, playerPath[0]);
        }
    }
}
