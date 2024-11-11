using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class com_On : MonoBehaviour
{
    private bool state;
    private bool isClose;
    private bool on;
    public GameObject Target;
    public GameObject text;

    void Start()
    {
        state = false;
        isClose = false;
        Screen.fullScreen = false; // 시작할 때 전체화면이 아님
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
                Debug.Log("on " + state);
            }
            else
            {
                Target.SetActive(true); // 생김
                state = true;
                Debug.Log("off " + state);
            }
        }

        // 마우스 클릭 시 전체화면 전환
        if(state == true)
        {
            if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭
            {
            Debug.Log("버튼 클릭");
                ToggleFullScreen();
            }
        }
        
    }

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

    private void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen; // 전체화면 모드 전환
        Debug.Log("Full Screen: " + Screen.fullScreen);
    }
}