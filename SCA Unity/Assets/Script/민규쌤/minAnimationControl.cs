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
        Debug.Log("********상점 UI 활성화됨... isTalking = true 설정");
        if (animator != null)
        {
            animator.SetBool("isTalking", true);
        }
        else
        {
            Debug.LogError("애니메이터가 연결되지 않음 Inspector에서 할당 필요.");
        }
        wasCursorLocked = Cursor.lockState == CursorLockMode.Locked; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

            firstPersonLook.enabled = false;

    }

    void OnDisable()
    {
        Debug.Log("*******상점 UI 비활성화됨... isTalking = false 설정");
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
