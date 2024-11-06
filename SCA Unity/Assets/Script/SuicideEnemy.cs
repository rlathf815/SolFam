using System.Collections;
using UnityEngine;

public class SuicideEnemy : MonoBehaviour
{
    public float aggroRange = 10f; // 어그로 범위
    public float followSpeed = 3f; // 추적 속도
    public float explosionDelay = 5f; // 폭발 딜레이 (5초)

    private Transform player; // 플레이어 위치
    private bool isChasing = false; // 추적 중인지 여부
    private bool isExploding = false; // 폭발 타이머가 시작됐는지 여부

    void Start()
    {
        // 플레이어 오브젝트 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 플레이어와의 거리 측정
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 어그로 범위 내에 플레이어가 있을 때 추적 시작
        if (distanceToPlayer <= aggroRange)
        {
            isChasing = true;
            if (!isExploding)
            {
                StartCoroutine(ExplodeAfterDelay());
                isExploding = true;
            }
        }

        // 추적 로직
        if (isChasing)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // 플레이어 방향으로 이동
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;
    }

    IEnumerator ExplodeAfterDelay()
    {
        // 5초 후 폭발
        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    void Explode()
    {
        // 폭발 효과 (여기에 파티클이나 사운드 효과를 추가 가능)
        Debug.Log("Boom! Enemy exploded.");

        // 폭발 후 오브젝트 소멸
        Destroy(gameObject);
    }
}