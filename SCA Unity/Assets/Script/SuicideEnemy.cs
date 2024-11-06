using System.Collections;
using UnityEngine;

public class SuicideEnemy : MonoBehaviour
{
    public float aggroRange = 10f; // ��׷� ����
    public float followSpeed = 3f; // ���� �ӵ�
    public float explosionDelay = 5f; // ���� ������ (5��)

    private Transform player; // �÷��̾� ��ġ
    private bool isChasing = false; // ���� ������ ����
    private bool isExploding = false; // ���� Ÿ�̸Ӱ� ���۵ƴ��� ����

    void Start()
    {
        // �÷��̾� ������Ʈ ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // �÷��̾���� �Ÿ� ����
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ��׷� ���� ���� �÷��̾ ���� �� ���� ����
        if (distanceToPlayer <= aggroRange)
        {
            isChasing = true;
            if (!isExploding)
            {
                StartCoroutine(ExplodeAfterDelay());
                isExploding = true;
            }
        }

        // ���� ����
        if (isChasing)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // �÷��̾� �������� �̵�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;
    }

    IEnumerator ExplodeAfterDelay()
    {
        // 5�� �� ����
        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    void Explode()
    {
        // ���� ȿ�� (���⿡ ��ƼŬ�̳� ���� ȿ���� �߰� ����)
        Debug.Log("Boom! Enemy exploded.");

        // ���� �� ������Ʈ �Ҹ�
        Destroy(gameObject);
    }
}