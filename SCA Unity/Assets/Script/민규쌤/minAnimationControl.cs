using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minAnimationControl : MonoBehaviour
{
    public Animator animator;
    public MonoBehaviour firstPersonLook;
    private bool wasCursorLocked;

    void OnEnable()
    {
        Debug.Log("********���� UI Ȱ��ȭ��... isTalking = true ����");
        if (animator != null)
        {
            animator.SetBool("isTalking", true);
        }
        else
        {
            Debug.LogError("�ִϸ����Ͱ� ������� ���� Inspector���� �Ҵ� �ʿ�.");
        }
        wasCursorLocked = Cursor.lockState == CursorLockMode.Locked; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

            firstPersonLook.enabled = false;

    }

    void OnDisable()
    {
        Debug.Log("*******���� UI ��Ȱ��ȭ��... isTalking = false ����");
        if (animator != null)
        {
            animator.SetBool("isTalking", false);
        }
        if (wasCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

            firstPersonLook.enabled = true;

    }
}
