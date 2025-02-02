using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class rms_nav : MonoBehaviour
{
    public float moveRadius = 10f; // ���Ͱ� �̵��� �� �ִ� ����
    public float waitTime = 2f; // ������ ���� �� ��� �ð�

    private NavMeshAgent agent;
    private Vector3 startPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position; // �ʱ� ��ġ ����
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            Vector3 randomDestination = GetRandomPoint(startPosition, moveRadius);
            agent.SetDestination(randomDestination);

            // ��ǥ ���� �������� ���
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // ���� �� ���� �ð� ���
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 10; i++) // �ִ� 10�� �õ�
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            randomPos.y = center.y; // ���̰� ����

            // NavMesh �� �����ϴ� ��ȿ�� ��ġ���� Ȯ��
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center; // ���� �� ���� ��ġ ��ȯ
    }
}
