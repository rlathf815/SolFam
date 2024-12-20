using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Touch : MonoBehaviour
{
    private bool isClose;
    public Transform player; // 플레이어의 Transform
    public float followDistance = 5.0f; // NPC가 플레이어와 유지할 거리
    public float followSpeed = 2.0f; // NPC의 이동 속도
    public float followConditionDistance = 10.0f; // NPC가 따라다닐 조건 거리
    public GameObject text;
    public GameObject cat;
    private float CreateTime = 0;

    //만나면 확률적으로 냥택을하고 아닐경우(플레이어에게 딜을 넣는다) 호감도작을 하여 호감스택을 쌓아야한다 호감작키[E]키 
    void Start()
    {
        isClose = false;
    }


    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("E키 입력");
            if (Random.value < 0.3f)
            {
                Debug.Log("냥택");
                text.SetActive(false);
                // 플레이어와의 거리 계산
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                // 조건을 만족할 때만 플레이어를 따라다님
                
                if (distanceToPlayer < followConditionDistance)
                {
                    Follow();
                }
            }
            else
            {
                //플레이어를 공격(데미지는 5? 10?)
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
        // 플레이어와의 방향 계산
        Vector3 direction = (player.position - transform.position).normalized;

        // NPC의 위치를 업데이트
        if (Vector3.Distance(transform.position, player.position) > followDistance)
        {
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }

    //텍스트
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
