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
    public GameObject cat;
    private float CreateTime = 0;

    //������ Ȯ�������� �������ϰ� �ƴҰ��(�÷��̾�� ���� �ִ´�) ȣ�������� �Ͽ� ȣ�������� �׾ƾ��Ѵ� ȣ����Ű[E]Ű 
    void Start()
    {
        isClose = false;
    }


    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("EŰ �Է�");
            if (Random.value < 0.3f)
            {
                Debug.Log("����");
                text.SetActive(false);
                // �÷��̾���� �Ÿ� ���
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                // ������ ������ ���� �÷��̾ ����ٴ�
                
                if (distanceToPlayer < followConditionDistance)
                {
                    Follow();
                }
            }
            else
            {
                //�÷��̾ ����(�������� 5? 10?)
                CreateTime += Time.deltaTime;
                Debug.Log((int)CreateTime);
                cat.SetActive(false);
                text.SetActive(false);
                if ((int)CreateTime >= 5)
                {
                    cat.SetActive(true);
                    text.SetActive(true);
                    CreateTime = 0;
                }
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

    //�ؽ�Ʈ
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
