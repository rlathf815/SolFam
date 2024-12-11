using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Touch : MonoBehaviour
{
    private bool isClose;
    public Transform player; // �÷��̾��� Transform
    public float followDistance = 5.0f; // NPC�� �÷��̾�� ������ �Ÿ�
    public float followSpeed = 2.0f; // NPC�� �̵� �ӵ�
    public float followConditionDistance = 10.0f; // NPC�� ����ٴ� ���� �Ÿ�
    public GameObject text;

    //������ Ȯ�������� �������ϰ� �ƴҰ��(�÷��̾�� ���� �ִ´�) ȣ�������� �Ͽ� ȣ�������� �׾ƾ��Ѵ� ȣ����Ű[E]Ű 
    void Start()
    {
        isClose = false;
    }


    void Update()
    {
        if (isClose = true && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("�÷��̾� ����");
            if(Random.value < 1.0f)
            {
                Debug.Log("EŰ �Է�");
                // �÷��̾���� �Ÿ� ���
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                // ������ ������ ���� �÷��̾ ����ٴ�
                
                if (distanceToPlayer < followConditionDistance)
                {
                    Debug.Log("���ä�");
                    Follow();
                }

            }
            else
            {
                //�÷��̾ ����(�������� 5? 10?) && ��� �����(1��? 3��?)
            }
        }
    }

    private void Follow()
    {
        // �÷��̾���� ���� ���
        Vector3 direction = (player.position - transform.position).normalized;

        // NPC�� ��ġ�� ������Ʈ
        if (Vector3.Distance(transform.position, player.position) > followDistance)
        {
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Player")
        {
            isClose = true;
            text.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Player")
        {
            isClose = false;
            text.SetActive(false);
        }
    }
}
