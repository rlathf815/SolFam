using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yun_button : MonoBehaviour
{
    public GameObject canvas;
    public static int moonjaeDab;

    public void PressButton(int a)
    {
        if (a == moonjaeDab)
        {
            isGood();
        }
        else
        {
            isBad();
        }
    }
    public void isGood()
    {
        yun_ui.openui = false;
        canvas.SetActive(false);
        Reset();
        Yun_Spawn.spawn = false;
    }

    public void isBad()
    {
        Nam_yun.punch = true;
        canvas.SetActive(false);
        StartCoroutine("Waitthe");
    }

    IEnumerator Waitthe()
    {
        yield return new WaitForSeconds(3.5f);
        PlayerStats.Instance.TakeDamage(3);
        Reset();
        yun_ui.openui = false;
        Yun_Spawn.spawn = false;
    }

    public static void Reset()
    {
        Jump.jumpStrength = 2;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonMovement.canRun = true;
        FirstPersonMovement.speed = 3f;
        FirstPersonLook.canlook = true;
        Crouch.movementSpeed = 2f;
        yeppy_player.catched = false;
    }
}
