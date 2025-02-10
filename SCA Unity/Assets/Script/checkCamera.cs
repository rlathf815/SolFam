using UnityEngine;

public class checkCamera : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Camera.main == null) // ���� ī�޶� ���� ��
        {
            Debug.LogError("�÷��̾� ������Ʈ�� ī�޶� �����ϴ�! �ڵ����� �Ҵ��մϴ�.");

            GameObject playerCamera = GameObject.FindWithTag("MainCamera");

            if (playerCamera != null)
            {
                Camera mainCam = playerCamera.GetComponent<Camera>();
                if (mainCam != null)
                {
                    mainCam.tag = "MainCamera"; // ī�޶� ������ �±� ����
                    Debug.Log("�÷��̾� ī�޶� 'MainCamera'�� ���� �Ϸ�!");
                }
                else
                {
                    Debug.LogError("'MainCamera' �±װ� ������ ī�޶� ������Ʈ�� �����ϴ�!");
                }
            }
            else
            {
                Debug.LogError(" �� ���� 'MainCamera' �±װ� �ִ� ī�޶� ������Ʈ�� �����ϴ�!");
            }
        }
        else
        {
            Debug.Log("Main Camera ���� ������!");
            Debug.Log("[SUCCESS] ������ Main Camera: " + Camera.main.name);
        }


    }
}
