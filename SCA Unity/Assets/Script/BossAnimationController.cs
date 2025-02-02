using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    public Animator animator; // 원장님 애니메이터
    public Transform player; // 플레이어 오브젝트
    public Transform headBone; // 원장님의 머리 본(Transform)
    public float headTurnSpeed = 5f; // 고개 회전 속도

    private void Update()
    {
        LookAtPlayer(); // 플레이어를 바라보도록 설정

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("space"); // 스페이스 누르면 애니메이션 변경
        }
    }

    private void LookAtPlayer()
    {
        if (player == null || headBone == null) return;

        // 몸 전체 회전 (몸 방향을 플레이어에게 맞춤)
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // 위아래 회전 방지
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3f);

        // 고개 회전 (headBone만 플레이어를 향하도록 설정)
        Vector3 headDirection = (player.position - headBone.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(headDirection);
        headBone.rotation = Quaternion.Slerp(headBone.rotation, lookRotation, Time.deltaTime * headTurnSpeed);
    }
}
