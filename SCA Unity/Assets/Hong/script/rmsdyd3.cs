using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public Transform spotlight;
    public float detectionRadius1 = 10f; // ���� ���� 1
    public float detectionRadius2 = 30f; // ���� ���� 2
    public float detectionRadius3 = 15f; // ���� ���� 3
    public float chaseSpeed = 5f;        // �߰� �ӵ�
    public float rotationSpeed = 5f;    // ȸ�� �ӵ�
    public float lostPlayerDelay = 3f;  // �÷��̾ �Ҿ��� �� ��� �ð�
    public float positionThreshold = 0.5f; // ������ ��ġ�� �����ߴٰ� �����ϴ� �Ÿ�

    public float viewAngle = 45f; // **�þ߰�**: �÷��̾ ������ �� �ִ� ����

    private float timeSinceLastSeen = 0f; // ���������� �÷��̾ �� �ð�
    private Vector3 lastKnownPosition;   // ���������� ������ �÷��̾� ��ġ
    private bool isPlayerInSight = false; // �÷��̾ ���� �þ߿� �ִ��� Ȯ��
    private List<Vector3> playerPath = new List<Vector3>(); // �÷��̾� ��� ����
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
                    // ���� 1 ������ �÷��̾� �߰�
                    isPlayerInSight = true;
                    timeSinceLastSeen = 0f;
                    lastKnownPosition = player.position;
                    ChasePlayer(player.position);
                    RotateTowards(player.position - transform.position);
                }
                else if (IsPlayerWithinRange(detectionRadius3))
                {
                    // ���� 3 ������ �÷��̾� �߰� (�ǽð� ����)
                    isPlayerInSight = false;
                    playerPath.Clear(); // ���� ��� �ʱ�ȭ
                    ChasePlayer(player.position);
                    RotateTowards(player.position - transform.position);
                }
                else if (IsPlayerWithinRange(detectionRadius2))
                {
                    // ���� 2 ������ �̵� ��θ� ���� �̵�
                    isPlayerInSight = false;
                    SavePlayerPath(); // �÷��̾� ��� ����
                    FollowPlayerPath();
                }
                else
                {
                    // ���� 2 �� 3���� ��� ���
                    timeSinceLastSeen += Time.deltaTime;

                    if (timeSinceLastSeen > lostPlayerDelay)
                    {
                        currentState = AIState.Idle;
                    }
                }
                spotlight.gameObject.SetActive(true); // �߰� �߿��� �׻� �ѱ�
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
        // �÷��̾��� ���� ��ġ�� ��ο� ����
        if (playerPath.Count == 0 || Vector3.Distance(player.position, playerPath[playerPath.Count - 1]) > positionThreshold)
        {
            playerPath.Add(player.position);
        }
    }

    void FollowPlayerPath()
    {
        // ��� ���� �̵�
        if (playerPath.Count > 0)
        {
            Vector3 targetPosition = playerPath[0];
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget <= positionThreshold)
            {
                // ����� ���� ������ �����ϸ� ����
                playerPath.RemoveAt(0);
            }
            else
            {
                // ��� ���� �̵�
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
                spotlight.gameObject.SetActive(true); // �߰� ���� ���� �׻� �ѱ�
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
