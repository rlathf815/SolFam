using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;              // 플레이어의 Transform
    public Transform spotlight;           // 스포트라이트의 Transform
    public float detectionRadius = 10f;   // 감지 반경
    public float chaseSpeed = 5f;         // 추적 속도
    public float checkInterval = 2f;      // 체크 간격
    public float avoidanceDistance = 1f;   // 벽 회피 거리

    private bool isChasing = false;       // 추적 중인지 여부

    void Start()
    {
        // 스포트라이트를 주기적으로 켜고 끄는 코루틴 시작
        StartCoroutine(ToggleSpotlight());
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
            LookAtPlayer(); // 플레이어를 바라보게 설정
            spotlight.gameObject.SetActive(true); // 스포트라이트 켜기
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
                    isChasing = true; // 플레이어를 감지하면 추적 시작
                }
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // 벽 회피 로직 추가
        Vector3 avoidanceDirection = Vector3.zero;

        // 벽 감지 및 회피
        if (IsWallNear(transform.position, avoidanceDistance))
        {
            avoidanceDirection = GetAvoidanceDirection(directionToPlayer);
            directionToPlayer += avoidanceDirection.normalized; // 회피 방향 추가
        }

        directionToPlayer.Normalize(); // 정규화
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
            if (hit.collider.CompareTag("Wall")) // 'Wall' 태그가 붙은 오브젝트
            {
                return true;
            }
        }
        return false;
    }

    bool IsWallNear(Vector3 position, float distance)
    {
        // 주위에 벽이 있는지 확인
        return Physics.Raycast(position, transform.forward, distance) ||
               Physics.Raycast(position, -transform.forward, distance) ||
               Physics.Raycast(position, transform.right, distance) ||
               Physics.Raycast(position, -transform.right, distance);
    }

    Vector3 GetAvoidanceDirection(Vector3 directionToPlayer)
    {
        // 벽을 피하는 방향 계산
        Vector3 right = Quaternion.Euler(0, 90, 0) * directionToPlayer; // 오른쪽 방향
        Vector3 left = Quaternion.Euler(0, -90, 0) * directionToPlayer; // 왼쪽 방향

        // 오른쪽과 왼쪽의 벽 거리 측정
        float rightDistance = GetDistanceToWall(transform.position + right, right);
        float leftDistance = GetDistanceToWall(transform.position + left, left);

        // 더 멀리 있는 쪽으로 회피
        return rightDistance > leftDistance ? left : right;
    }

    float GetDistanceToWall(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction.normalized, out hit, avoidanceDistance))
        {
            return hit.distance; // 벽까지의 거리 반환
        }
        return avoidanceDistance; // 벽이 없으면 최대 회피 거리 반환
    }

    System.Collections.IEnumerator ToggleSpotlight()
    {
        while (true)
        {
            if (!isChasing)
            {
                spotlight.gameObject.SetActive(!spotlight.gameObject.activeSelf); // 스포트라이트 켜기/끄기
                yield return new WaitForSeconds(2f); // 2초 대기
            }
            else
            {
                yield return null; // 추격 중일 경우 대기 없이 계속
            }
        }
    }
}
