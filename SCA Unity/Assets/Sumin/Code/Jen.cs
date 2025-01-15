using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Jen : MonoBehaviour
{
 
    public Transform player; // 플레이어의 Transform을 할당할 변수
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
            // 오브젝트를 플레이어의 위치로 즉시 이동
            Vector3 playerPosition = player.position;
            Vector3 playerForward = player.forward;

            // 플레이어의 앞쪽으로 일정 거리만큼 이동합니다.
            transform.position = playerPosition + playerForward * distance;
            Coffee_I = 0;
            MenSeter_I = 0;
            StartCoroutine(DisappearAfterTime(3f, jen));
        }
    
    }

    // 코루틴 정의
    public IEnumerator DisappearAfterTime(float time,GameObject target)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(time);

        // 게임 오브젝트 비활성화
        Renderer jenRenderer = jen.GetComponent<Renderer>();
        jenRenderer.enabled = false;
        transform.position = new Vector3(0,10,0);
    }

}