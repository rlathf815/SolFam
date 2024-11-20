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
    public float lostPlayerDelay = 3f; // �÷��̾ ���� �� Idle�� ��ȯ�Ǵ� �ð�

    private float timeSinceLastSeen = 0f; // ���������� �÷��̾ �� �ð�
    private Vector3 lastKnownPosition;   // ���������� ������ �÷��̾� ��ġ
    private bool isPlayerInSight = false; // �÷��̾ ���� �þ߿� �ִ��� Ȯ��
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
                    lastKnownPosition = player.position; // �÷��̾� ��ġ ����
                    ChasePlayer(player.position);
                }
                else
                {
                    isPlayerInSight = false;
                    timeSinceLastSeen += Time.deltaTime;

                    if (timeSinceLastSeen > lostPlayerDelay)
                    {
                        // ���� �ð��� ������ Idle ���·� ����
                        currentState = AIState.Idle;
                    }
                    else
                    {
                        // �÷��̾ ������ ������ ������ ��ġ�� ����
                        ChasePlayer(lastKnownPosition);
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

            if (distanceToPlayer <= detectionRadius)
            {
                currentState = AIState.Chasing;
                timeSinceLastSeen = 0f; // ���� ���� �� �ʱ�ȭ
                lastKnownPosition = player.position; // �÷��̾��� ���� ��ġ ����
            }
        }
    }

    void ChasePlayer(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // �� ���� �� ȸ�� ����
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, obstacleDetectionDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Vector3 avoidDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
                directionToTarget = (directionToTarget + avoidDirection).normalized;
            }
        }

        // �̵�
        transform.position += directionToTarget * chaseSpeed * Time.deltaTime;

        // �̵� �������� �ü� ȸ��
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
                spotlight.gameObject.SetActive(true); // �߰� ���� ���� �׻� �ѱ�
                yield return null;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // �þ߰� �׸���
        Gizmos.color = Color.red;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle, 0) * transform.forward * detectionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle, 0) * transform.forward * detectionRadius;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // ��ֹ� ���� �Ÿ� ǥ��
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * obstacleDetectionDistance);
    }
}
