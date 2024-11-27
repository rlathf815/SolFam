using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB_On : MonoBehaviour
{
    private bool state; // USB ����
    private bool isClose; // �÷��̾ ������ �ִ��� ����
    private bool USB_Cube;
    public GameObject Target; // Ȱ��ȭ�� ���� ������Ʈ
    public GameObject text; // �ؽ�Ʈ ������Ʈ
    public GameObject Cube; //ť�� ������Ʈ

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
        if (isClose && Input.GetKeyDown(KeyCode.E)) // E Ű ����
        {
            if (state == true)
            {
                Target.SetActive(false); // �����
                state = false;
                Cube.SetActive(true);
                USB_Cube = true;
                text.SetActive(true);
                Debug.Log("on " + state);
            }
            else
            {
                Target.SetActive(true); // ����
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