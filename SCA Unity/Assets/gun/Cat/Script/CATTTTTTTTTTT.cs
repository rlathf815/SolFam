using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CATTTTTTTTTTT : MonoBehaviour
{
    public GameObject text;
    public Transform player; // �÷��̾��� Transform
    public float disappearTime = 5f; // ������� �ð�
    public GameObject cat;
    public Animator controller;

    private bool isFollowing = false; // ���� ������� �ִ��� ����
    private Renderer objectRenderer; // ������Ʈ�� Renderer
    private bool isClose;
    void Start()
    {
        isClose = false;
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = true; // ó������ ���̵��� ����
        controller = GetComponent<Animator>();
    }

    void Update()
    {
        // E Ű�� ������ ��
        if (isClose && Input.GetKeyDown(KeyCode.E))
        {
            // isFollowing�� true�� ��� �������� ����
            if (isFollowing)
            {
                return; // �ƹ� ������ ���� ����
            }

            // 30% Ȯ���� �������
            if (Random.value <= 0.9f) // 30% Ȯ��
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
            text.SetActive(false);
            Follow();
        }
    }

    void Follow()
    {
        // �÷��̾��� ��ġ�� �ε巴�� �̵�
        cat.transform.position = Vector3.Lerp(cat.transform.position, player.position, Time.deltaTime);

        // �÷��̾ �ٶ󺸵��� ȸ��
        Vector3 direction = (player.position - cat.transform.position).normalized; // �÷��̾� ���� ����
        if (direction != Vector3.zero) // ���� ���Ͱ� 0�� �ƴ� ���� ȸ��
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction); // ���� ���͸� ������� ȸ�� ����
            cat.transform.rotation = Quaternion.Slerp(cat.transform.rotation, lookRotation, Time.deltaTime * 5f); // �ε巴�� ȸ��
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
            Debug.Log("trigger entered");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isClose = false;
            text.SetActive(false);
            Debug.Log("trigger exit");
        }
    }
}