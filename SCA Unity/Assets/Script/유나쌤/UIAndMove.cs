using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yun_ui : MonoBehaviour
{
    public static bool uiopen = false;
    public GameObject canvas;
    void Update()
    {
        if (uiopen == true)
        {
            canvas.SetActive(true);
            FirstPersonMovement.speed = 0f;
        }
        else
        {
            canvas.SetActive(false);
            FirstPersonMovement.speed = 5f;
        }
    }
}
