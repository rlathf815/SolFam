using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jen : MonoBehaviour
{
    public int point;
    public Transform player; // �÷��̾��� Transform�� �Ҵ��� ����
    public int MenSeter_I;
    public int Coffee_I;

    public Transform Player;
    public float distance = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (point == 0)
        {
            // ������Ʈ�� �÷��̾��� ��ġ�� ��� �̵�
            Vector3 playerPosition = player.position;
            Vector3 playerForward = player.forward;

            // �÷��̾��� �������� ���� �Ÿ���ŭ �̵��մϴ�.
            transform.position = playerPosition + playerForward * distance;
            Coffee_I =0;
            MenSeter_I=0;
        }
    }
}