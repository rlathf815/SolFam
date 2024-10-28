using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;              // �÷��̾��� Transform
    public Transform spotlight;           // ����Ʈ����Ʈ�� Transform
    public float detectionRadius = 10f;   // ���� �ݰ� (����Ʈ����Ʈ�� �÷��̾� ���� �Ÿ�)
    public float chaseSpeed = 5f;         // ���� �ӵ�
    public float checkInterval = 2f;      // üũ ����

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
        }

        DetectPlayer();
    }

    void DetectPlayer()
    {
        // ����Ʈ����Ʈ�� ���� ���� ���� �÷��̾� ����
        if (spotlight.gameObject.activeSelf)
        {
            float distanceToPlayer = Vector3.Distance(spotlight.position, player.position);

            // ������ ���� ���� ���� �ִ��� Ȯ��
            if (distanceToPlayer <= detectionRadius)
            {
                // �÷��̾�� ����Ʈ����Ʈ�� �մ� ���� ���� ���� ����
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
        transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
    }

    void LookAtPlayer()
    {
        // �÷��̾ �ٶ󺸵��� ȸ��
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // �ε巯�� ȸ��
    }

    bool IsWallBetween(Vector3 from, Vector3 to)
    {
        RaycastHit hit;
        if (Physics.Raycast(from, to - from, out hit, Vector3.Distance(from, to)))
        {
            // ���� ���� ��ü�� ���̶�� true ��ȯ
            if (hit.collider.CompareTag("Wall")) // 'Wall' �±װ� ���� ������Ʈ
            {
                return true;
            }
        }
        return false;
    }

    System.Collections.IEnumerator ToggleSpotlight()
    {
        while (true)
        {
            spotlight.gameObject.SetActive(true); // ����Ʈ����Ʈ �ѱ�
            yield return new WaitForSeconds(2f); // 2�� ���
            spotlight.gameObject.SetActive(false); // ����Ʈ����Ʈ ����
            yield return new WaitForSeconds(2f); // 2�� ���
        }
    }
}
