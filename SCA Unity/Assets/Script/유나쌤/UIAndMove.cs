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
                    moon.text = "  ��ǻ�Ͱ� ���̷����� �������� �� ġ�����?";
                    dab1.text = "1.��ǻ�͸� �μ���";
                    dab2.text = "2.����� �����Ѵ�";
                    dab3.text = "3.�⵵�� �Ѵ�";
                }
                else
                {
                    moon.text = "  ��ǻ�͸� �ʱ�ȭ �ϴ� �ùٸ� �����?";
                    dab1.text = "1.��ǻ�͸� �μ���";
                    dab2.text = "2.������ �õ��Ѵ�";
                    dab3.text = "3.������� ���";
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
