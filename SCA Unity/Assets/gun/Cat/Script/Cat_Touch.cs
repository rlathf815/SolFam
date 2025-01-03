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
    private float CreateTime;

    //만나면 확률적으로 냥택을하고 아닐경우(플레이어에게 딜을 넣는다) 호감도작을 하여 호감스택을 쌓아야한다 호감작키[E]키
    void Start()
    {
        isClose = false;
        CreateTime = 0;
    }


    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("E키 입력");
            if (Random.value < 0.3f)
            {
                Debug.Log("냥택");
                text.SetActive(false);

                //고양이가 따라오는 실시간으로 플레이어 위치 값을 찾아서 이동
                cat.transform.Translate(new Vector3(0.0f, 0.0f, 3.0f * Time.deltaTime));

            }
            else if (Random.value <0.7f)
            {
                Debug.Log("실패");
                //플레이어를 공격(데미지는 5? 10?)

                //사라졌다가 5초 뒤에 생성
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
