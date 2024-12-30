using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yun_button : MonoBehaviour
{
    public GameObject canvas;
    public void isGood()
    {
        yun_ui.openui = false;
        canvas.SetActive(false);
        Reset();
        Yun_Spawn.spawn = false;
    }

    public void isBad()
    {
        yun_ui.openui = false;
        canvas.SetActive(false);
        Reset();
        Yun_Spawn.spawn = false;
    }

    public static void Reset()
    {
        Jump.jumpStrength = 2;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonMovement.canRun = true;
        FirstPersonMovement.speed = 3f;
        Crouch.movementSpeed = 2f;
        yeppy_player.catched = false;
    }
}
