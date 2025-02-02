using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class com_On : MonoBehaviour
{
    private bool state;
    private bool CamState;
    private bool isClose;
    private bool on;
    private bool PcClose;
    private bool PlayerC;
    public GameObject Target;
    public GameObject text;
    public GameObject PcCam;
    public GameObject PlayerCam;

    void Start()
    {
        state = false; //화면상태
        CamState = false; //화면상태
        isClose = false;
        PlayerC = true;
        PcClose = false;
        Cursor.visible = false; // 기본 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
    }

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
        if (state == true && isClose == true)
        {
            if (Input.GetKeyDown(KeyCode.F)&& CamState) 
            {
                //Debug.Log("왼쪽 버튼 클릭");
                PlayerCam.SetActive(false);
                PlayerC = false;
                PcCam.SetActive(true);
                PcClose = true;
                Cursor.visible = true; // 하드웨어 커서 표시
                Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
            }
            else if (Input.GetMouseButtonDown(1)) // 오른쪽 마우스 버튼 클릭
            {
                Debug.Log("오른쪽 버튼 클릭");
                PlayerCam.SetActive(true);
                PlayerC = true;
                PcCam.SetActive(false);
                PcClose = false;
                Cursor.visible = false; // 하드웨어 커서 숨기기
                Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
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
}