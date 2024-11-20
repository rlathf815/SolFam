using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB_On : MonoBehaviour
{
    private bool state; // USB ����
    private bool isClose; // �÷��̾ ������ �ִ��� ����
    private bool canPressE; // E Ű�� ���� �� �ִ��� ����
    public GameObject Target; // Ȱ��ȭ�� ���� ������Ʈ
    public GameObject text; // �ؽ�Ʈ ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        state = false;
        isClose = false;
        canPressE = true; // �ʹݿ� E Ű�� ���� �� �ֵ��� ����
        text.SetActive(false); // �ʱ� �ؽ�Ʈ ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && canPressE && Input.GetKeyDown(KeyCode.E)) // E Ű ����
        {
            Debug.Log("E " + state);
            state = !state; // ���¸� ���
            Target.SetActive(state); // ���¿� ���� Ÿ�� Ȱ��ȭ/��Ȱ��ȭ
            Debug.Log("USB state: " + state);

            // �ؽ�Ʈ ��Ȱ��ȭ
            text.SetActive(false);

            // E Ű�� ���� �� �� �̻� ���� �� ������ ����
            canPressE = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            isClose = true;
            text.SetActive(true); // �÷��̾ ������ ���� �� �ؽ�Ʈ Ȱ��ȭ
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            isClose = false;
            text.SetActive(false); // �÷��̾ �־����� �� �ؽ�Ʈ ��Ȱ��ȭ
        }
    }
}