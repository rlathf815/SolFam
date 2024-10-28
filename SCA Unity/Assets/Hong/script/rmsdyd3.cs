using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;              // �÷��̾��� Transform
    public Transform spotlight;           // ����Ʈ����Ʈ�� Transform
    public float detectionRadius = 10f;   // ���� �ݰ�
    public float chaseSpeed = 5f;         // ���� �ӵ�
    public float checkInterval = 2f;      // üũ ����
    public float avoidanceDistance = 1f;   // �� ȸ�� �Ÿ�

    private bool isChasing = false;       // ���� ������ ����

    void Start()
    {
        // ����Ʈ����Ʈ�� �ֱ������� �Ѱ� ���� �ڷ�ƾ ����
        StartCoroutine(ToggleSpotlight());
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
            LookAtPlayer(); // �÷��̾ �ٶ󺸰� ����
            spotlight.gameObject.SetActive(true); // ����Ʈ����Ʈ �ѱ�
        }
        else
        {
            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        if (spotlight.gameObject.activeSelf)
        {
            float distanceToPlayer = Vector3.Distance(spotlight.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                if (!IsWallBetween(spotlight.position, player.position))
                {
                    isChasing = true; // �÷��̾ �����ϸ� ���� ����
                }
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // �� ȸ�� ���� �߰�
        Vector3 avoidanceDirection = Vector3.zero;

        // �� ���� �� ȸ��
        if (IsWallNear(transform.position, avoidanceDistance))
        {
            avoidanceDirection = GetAvoidanceDirection(directionToPlayer);
            directionToPlayer += avoidanceDirection.normalized; // ȸ�� ���� �߰�
        }

        directionToPlayer.Normalize(); // ����ȭ
        transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
    }

    void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    bool IsWallBetween(Vector3 from, Vector3 to)
    {
        RaycastHit hit;
        if (Physics.Raycast(from, to - from, out hit, Vector3.Distance(from, to)))
        {
            if (hit.collider.CompareTag("Wall")) // 'Wall' �±װ� ���� ������Ʈ
            {
                return true;
            }
        }
        return false;
    }

    bool IsWallNear(Vector3 position, float distance)
    {
        // ������ ���� �ִ��� Ȯ��
        return Physics.Raycast(position, transform.forward, distance) ||
               Physics.Raycast(position, -transform.forward, distance) ||
               Physics.Raycast(position, transform.right, distance) ||
               Physics.Raycast(position, -transform.right, distance);
    }

    Vector3 GetAvoidanceDirection(Vector3 directionToPlayer)
    {
        // ���� ���ϴ� ���� ���
        Vector3 right = Quaternion.Euler(0, 90, 0) * directionToPlayer; // ������ ����
        Vector3 left = Quaternion.Euler(0, -90, 0) * directionToPlayer; // ���� ����

        // �����ʰ� ������ �� �Ÿ� ����
        float rightDistance = GetDistanceToWall(transform.position + right, right);
        float leftDistance = GetDistanceToWall(transform.position + left, left);

        // �� �ָ� �ִ� ������ ȸ��
        return rightDistance > leftDistance ? left : right;
    }

    float GetDistanceToWall(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction.normalized, out hit, avoidanceDistance))
        {
            return hit.distance; // �������� �Ÿ� ��ȯ
        }
        return avoidanceDistance; // ���� ������ �ִ� ȸ�� �Ÿ� ��ȯ
    }

    System.Collections.IEnumerator ToggleSpotlight()
    {
        while (true)
        {
            if (!isChasing)
            {
                spotlight.gameObject.SetActive(!spotlight.gameObject.activeSelf); // ����Ʈ����Ʈ �ѱ�/����
                yield return new WaitForSeconds(2f); // 2�� ���
            }
            else
            {
                yield return null; // �߰� ���� ��� ��� ���� ���
            }
        }
    }
}
