using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yun_button : MonoBehaviour
{
    public GameObject Yui;
    void isGood()
    {
        yun_ui.cool = false;
        yeppy_player.catched = false;
        yun_ui.uiopen = false;
        FirstPersonMovement.canRun = true;
        min_detect.dontmovescreen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Yui.SetActive(false);
    }
    void isBad()
    {
        yun_ui.cool = false;
        yeppy_player.catched = false;
        yun_ui.uiopen = false;
        FirstPersonMovement.canRun = true;
        min_detect.dontmovescreen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Yui.SetActive(false);
    }
}
