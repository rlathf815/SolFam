using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class yun_ui : MonoBehaviour
{
    public static bool uiopen = false;
    public static bool del = false;
    bool cool = false;
    public GameObject canvas;
    public TextMeshProUGUI moon;
    public TextMeshProUGUI dab1;
    public TextMeshProUGUI dab2;
    public TextMeshProUGUI dab3;
    void Update()
    {
        if (uiopen == true)
        {
            if (cool == false)
            {
                cool = true;
                canvas.SetActive(true);
                FirstPersonMovement.speed = 0f;
                if (Random.Range(0, 2) == 1)
                {
                    moon.text = "  컴퓨터가 바이러스에 감염됐을 때 치료법은?";
                    dab1.text = "1.컴퓨터를 부순다";
                    dab2.text = "2.백신을 실행한다";
                    dab3.text = "3.기도를 한다";
                }
                else
                {
                    moon.text = "  컴퓨터를 초기화 하는 올바른 방법은?";
                    dab1.text = "1.컴퓨터를 부순다";
                    dab2.text = "2.포맷을 시도한다";
                    dab3.text = "3.랜섬웨어를 깐다";
                }
            }
        }
        else
        {
            canvas.SetActive(false);
            FirstPersonMovement.speed = 5f;
        }
        if (cool == true)
        {
            if(Input.GetKey(KeyCode.Alpha2)||Input.GetKey(KeyCode.Keypad2)) {
                cool = false;
                uiopen = false;
                del = true;
            }
            if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1)){
                cool = false;
                uiopen = false;
                del = true;
            }
            if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
            {
                cool = false;
                uiopen = false;
                del = true;
            }
        }
    }
}
