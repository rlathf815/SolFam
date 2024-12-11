using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class min_button : MonoBehaviour
{
    public GameObject Schang;
    public GameObject Epress;
    public void CloseClick()
    {
        Schang.SetActive(false);
        Epress.SetActive(true);
        min_detect.dontmovescreen = false;
        Jump.jumpStrength = 2;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yeppy_player.catched = false;
        FirstPersonMovement.canRun = true;
        min_detect.detect = false;
        min_detect.aniplay = false;
    }
}
