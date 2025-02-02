using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class rms_nav : MonoBehaviour
{
    public float moveRadius = 10f; // 몬스터가 이동할 수 있는 범위
    public float waitTime = 2f; // 목적지 도착 후 대기 시간

    private NavMeshAgent agent;
    private Vector3 startPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position; // 초기 위치 저장
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            Vector3 randomDestination = GetRandomPoint(startPosition, moveRadius);
            agent.SetDestination(randomDestination);

            // 목표 지점 도착까지 대기
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // 도착 후 일정 시간 대기
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 10; i++) // 최대 10번 시도
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            randomPos.y = center.y; // 높이값 유지

            // NavMesh 상에 존재하는 유효한 위치인지 확인
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center; // 실패 시 현재 위치 반환
    }
}
