using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jen : MonoBehaviour
{
    public int point;
    public Transform player; // �÷��̾��� Transform�� �Ҵ��� ����
    public int MenSeter_I;
    public int Coffee_I;
    public int MenSeter_o;
    public int Coffee_o;

    // Update is called once per frame
    void Update()
    {
        MenSeter_o = MenSeter_I;
        Coffee_o = Coffee_I;
        if (point == 0)
        {
            // ������Ʈ�� �÷��̾��� ��ġ�� ��� �̵�
            transform.position = player.position;
            Coffee_I=0;
            MenSeter_I=0;
        }
    }
}