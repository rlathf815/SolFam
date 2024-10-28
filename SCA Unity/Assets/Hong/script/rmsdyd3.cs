using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;              // 플레이어의 Transform
    public Transform spotlight;           // 스포트라이트의 Transform
    public float detectionRadius = 10f;   // 감지 반경 (스포트라이트와 플레이어 간의 거리)
    public float chaseSpeed = 5f;         // 추적 속도
    public float checkInterval = 2f;      // 체크 간격

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
        }

        DetectPlayer();
    }

    void DetectPlayer()
    {
        // 스포트라이트가 켜져 있을 때만 플레이어 감지
        if (spotlight.gameObject.activeSelf)
        {
            float distanceToPlayer = Vector3.Distance(spotlight.position, player.position);

            // 설정된 감지 범위 내에 있는지 확인
            if (distanceToPlayer <= detectionRadius)
            {
                // 플레이어와 스포트라이트를 잇는 선을 따라 벽을 감지
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
        transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
    }

    void LookAtPlayer()
    {
        // 플레이어를 바라보도록 회전
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 부드러운 회전
    }

    bool IsWallBetween(Vector3 from, Vector3 to)
    {
        RaycastHit hit;
        if (Physics.Raycast(from, to - from, out hit, Vector3.Distance(from, to)))
        {
            // 만약 맞은 물체가 벽이라면 true 반환
            if (hit.collider.CompareTag("Wall")) // 'Wall' 태그가 붙은 오브젝트
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
            spotlight.gameObject.SetActive(true); // 스포트라이트 켜기
            yield return new WaitForSeconds(2f); // 2초 대기
            spotlight.gameObject.SetActive(false); // 스포트라이트 끄기
            yield return new WaitForSeconds(2f); // 2초 대기
        }
    }
}
