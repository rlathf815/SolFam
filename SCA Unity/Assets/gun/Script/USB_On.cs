using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB_On : MonoBehaviour
{
    private bool state; // USB 상태
    private bool isClose; // 플레이어가 가까이 있는지 여부
    private bool USB_Cube;
    public GameObject Target; // 활성화할 게임 오브젝트
    public GameObject text; // 텍스트 오브젝트
    public GameObject Cube; //큐브 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        USB_Cube = true;
        state = false;
        isClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) // E 키 누름
        {
            if (state == true)
            {
                Target.SetActive(false); // 사라짐
                state = false;
                Cube.SetActive(true);
                USB_Cube = true;
                text.SetActive(true);
                Debug.Log("on " + state);
            }
            else
            {
                Target.SetActive(true); // 생김
                state = true;
                Cube.SetActive(false);
                USB_Cube= false;
                text.SetActive(false);
                Debug.Log("off " + state);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            isClose = true;
            text.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            isClose = false;
            text.SetActive(false);
        }
    }
}