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
        Screen.fullScreen = false; // ������ �� ��üȭ���� �ƴ�
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) // E Ű ����
        {

            if (state == true)
            {
                Target.SetActive(false); // �����
                state = false;
                Debug.Log("on " + state);
            }
            else
            {
                Target.SetActive(true); // ����
                state = true;
                Debug.Log("off " + state);
            }
        }

        // ���콺 Ŭ�� �� ��üȭ�� ��ȯ
        if(state == true)
        {
            if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
            {
            Debug.Log("��ư Ŭ��");
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
        Screen.fullScreen = !Screen.fullScreen; // ��üȭ�� ��� ��ȯ
        Debug.Log("Full Screen: " + Screen.fullScreen);
    }
}