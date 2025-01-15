using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Jen : MonoBehaviour
{
 
    public Transform player; // �÷��̾��� Transform�� �Ҵ��� ����
    public int MenSeter_I;
    public int Coffee_I;

    public Transform Player;
    public float distance = 1.0f;

    public GameObject jen;

    void Start()
    {
        Renderer jenRenderer = jen.GetComponent<Renderer>();
        jenRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer jenRenderer = jen.GetComponent<Renderer>();
        if (Rest.point == 0)
        {
            jenRenderer.enabled = true;
            // ������Ʈ�� �÷��̾��� ��ġ�� ��� �̵�
            Vector3 playerPosition = player.position;
            Vector3 playerForward = player.forward;

            // �÷��̾��� �������� ���� �Ÿ���ŭ �̵��մϴ�.
            transform.position = playerPosition + playerForward * distance;
            Coffee_I = 0;
            MenSeter_I = 0;
            StartCoroutine(DisappearAfterTime(3f, jen));
        }
    
    }

    // �ڷ�ƾ ����
    public IEnumerator DisappearAfterTime(float time,GameObject target)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(time);

        // ���� ������Ʈ ��Ȱ��ȭ
        Renderer jenRenderer = jen.GetComponent<Renderer>();
        jenRenderer.enabled = false;
        transform.position = new Vector3(0,10,0);
    }

}