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
        state = false; //ȭ�����
        CamState = false; //ȭ�����
        isClose = false;
        PlayerC = true;
        PcClose = false;
        Cursor.visible = false; // �⺻ Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked; // Ŀ�� ���
    }

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
        if (state == true && isClose == true)
        {
            if (Input.GetKeyDown(KeyCode.F)&& CamState) 
            {
                //Debug.Log("���� ��ư Ŭ��");
                PlayerCam.SetActive(false);
                PlayerC = false;
                PcCam.SetActive(true);
                PcClose = true;
                Cursor.visible = true; // �ϵ���� Ŀ�� ǥ��
                Cursor.lockState = CursorLockMode.None; // Ŀ�� ��� ����
            }
            else if (Input.GetMouseButtonDown(1)) // ������ ���콺 ��ư Ŭ��
            {
                Debug.Log("������ ��ư Ŭ��");
                PlayerCam.SetActive(true);
                PlayerC = true;
                PcCam.SetActive(false);
                PcClose = false;
                Cursor.visible = false; // �ϵ���� Ŀ�� �����
                Cursor.lockState = CursorLockMode.Locked; // Ŀ�� ���
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