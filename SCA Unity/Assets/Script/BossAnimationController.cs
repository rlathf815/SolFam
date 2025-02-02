using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    public Animator animator; // ����� �ִϸ�����
    public Transform player; // �÷��̾� ������Ʈ
    public Transform headBone; // ������� �Ӹ� ��(Transform)
    public float headTurnSpeed = 5f; // �� ȸ�� �ӵ�

    private void Update()
    {
        LookAtPlayer(); // �÷��̾ �ٶ󺸵��� ����

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("space"); // �����̽� ������ �ִϸ��̼� ����
        }
    }

    private void LookAtPlayer()
    {
        if (player == null || headBone == null) return;

        // �� ��ü ȸ�� (�� ������ �÷��̾�� ����)
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // ���Ʒ� ȸ�� ����
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3f);

        // �� ȸ�� (headBone�� �÷��̾ ���ϵ��� ����)
        Vector3 headDirection = (player.position - headBone.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(headDirection);
        headBone.rotation = Quaternion.Slerp(headBone.rotation, lookRotation, Time.deltaTime * headTurnSpeed);
    }
}
