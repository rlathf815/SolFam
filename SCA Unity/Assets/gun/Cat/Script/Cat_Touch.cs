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
    private float CreateTime;

    //������ Ȯ�������� �������ϰ� �ƴҰ��(�÷��̾�� ���� �ִ´�) ȣ�������� �Ͽ� ȣ�������� �׾ƾ��Ѵ� ȣ����Ű[E]Ű
    void Start()
    {
        isClose = false;
        CreateTime = 0;
    }


    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("EŰ �Է�");
            if (Random.value < 0.3f)
            {
                Debug.Log("����");
                text.SetActive(false);

                //����̰� ������� �ǽð����� �÷��̾� ��ġ ���� ã�Ƽ� �̵�
                cat.transform.Translate(new Vector3(0.0f, 0.0f, 3.0f * Time.deltaTime));

            }
            else if (Random.value <0.7f)
            {
                Debug.Log("����");
                //�÷��̾ ����(�������� 5? 10?)

                //������ٰ� 5�� �ڿ� ����
                cat.SetActive(false);
                text.SetActive(false);
                if (CreateTime >= 5)
                {
                    cat.SetActive(true);
                    text.SetActive(true);
                    CreateTime = 0;
                }
                else
                {
                    CreateTime += Time.deltaTime;
                    Debug.Log(CreateTime);
                }
                
            }
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
