using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jen : MonoBehaviour
{
    public int point;
    public Transform player; // 플레이어의 Transform을 할당할 변수
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
            // 오브젝트를 플레이어의 위치로 즉시 이동
            transform.position = player.position;
            Coffee_I=0;
            MenSeter_I=0;
        }
    }
}