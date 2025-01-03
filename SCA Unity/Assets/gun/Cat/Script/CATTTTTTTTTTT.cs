using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CATTTTTTTTTTT : MonoBehaviour
{
    public GameObject text;
    public Transform player; // �÷��̾��� Transform
    public float disappearTime = 5f; // ������� �ð�

    private bool isFollowing = false; // ���� ������� �ִ��� ����
    private Renderer objectRenderer; // ������Ʈ�� Renderer
    private bool isClose;

    void Start()
    {
        isClose = false;
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = true; // ó������ ���̵��� ����
    }

    void Update()
    {
        // E Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.E))
        {
            // isFollowing�� true�� ��� �������� ����
            if (isFollowing)
            {
                return; // �ƹ� ������ ���� ����
            }

            // 30% Ȯ���� �������
            if (Random.value <= 0.3f) // 30% Ȯ��
            {
                isFollowing = true;
                Debug.Log("Following the player.");
            }
            else
            {
                isFollowing = false;
                Debug.Log("Not following. Starting disappear coroutine.");
                StartCoroutine(DisappearAndReappear());
            }
        }

        // �������
        if (isFollowing)
        {
            Follow();
        }
    }

    void Follow()
    {
        // �÷��̾��� ��ġ�� �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime);

        // �÷��̾ �ٶ󺸵��� ȸ��
        Vector3 direction = (player.position - transform.position).normalized; // �÷��̾� ���� ����
        if (direction != Vector3.zero) // ���� ���Ͱ� 0�� �ƴ� ���� ȸ��
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction); // ���� ���͸� ������� ȸ�� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // �ε巴�� ȸ��
        }
    }

    private IEnumerator DisappearAndReappear()
    {
        // ������Ʈ�� ������� ��
        Debug.Log("Disappearing...");
        objectRenderer.enabled = false;
        yield return new WaitForSeconds(disappearTime);
        // ������Ʈ�� �ٽ� ��Ÿ���� ��
        Debug.Log("Reappearing...");
        objectRenderer.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isClose = true;
            text.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isClose = false;
            text.SetActive(false);
        }
    }
}