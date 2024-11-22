using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jen : MonoBehaviour
{
    public int point;
    public Transform player; // 플레이어의 Transform을 할당할 변수
    public int MenSeter_I;
    public int Coffee_I;

    public Transform Player;
    public float distance = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (point == 0)
        {
            // 오브젝트를 플레이어의 위치로 즉시 이동
            Vector3 playerPosition = player.position;
            Vector3 playerForward = player.forward;

            // 플레이어의 앞쪽으로 일정 거리만큼 이동합니다.
            transform.position = playerPosition + playerForward * distance;
            Coffee_I =0;
            MenSeter_I=0;
        }
    }
}