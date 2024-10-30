using UnityEngine;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour
{
    public Transform player;              // �÷��̾��� Transform
    public Transform spotlight;           // ����Ʈ����Ʈ�� Transform
    public float detectionRadius = 10f;   // ���� �ݰ�
    public float chaseSpeed = 5f;         // ���� �ӵ�
    public float pathFollowDistance = 0.5f; // ��� ���󰡱� �Ÿ�

    private bool isChasing = false;       // ���� ������ ����
    private List<Vector3> playerPath;     // �÷��̾� ��� ����

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
            RecordPlayerPosition(); // �÷��̾� ��ġ�� ��� ���
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
                isChasing = true; // �÷��̾ �����ϸ� ���� ����
                RecordPlayerPosition(); // ���� �� �÷��̾� ��ġ ��� ����
            }
        }
    }

    void FollowPlayerPath()
    {
        if (playerPath.Count == 0) return;

        Vector3 targetPosition = playerPath[0];
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // �̵�
        if (directionToTarget != Vector3.zero)
        {
            transform.position += directionToTarget * chaseSpeed * Time.deltaTime;
        }

        // ��ǥ ��ġ�� �����ϸ� ��ο��� ����
        if (Vector3.Distance(transform.position, targetPosition) < pathFollowDistance)
        {
            playerPath.RemoveAt(0);
        }
    }

    void RecordPlayerPosition()
    {
        // �÷��̾� ��ġ�� ���
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
