using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class yun_ui : MonoBehaviour
{
    public static bool uiopen = false;
    public static bool del = false;
    public static bool cool = false;
    public GameObject canvas;
    public TextMeshProUGUI moon;
    public TextMeshProUGUI dab1;
    public TextMeshProUGUI dab2;
    public TextMeshProUGUI dab3;
    void Update()
    {
        if (uiopen == true)
        {
            if (cool == false&&yeppy_player.catched==false)
            {
                cool = true;
                canvas.SetActive(true);
                FirstPersonMovement.speed = 0f;
                FirstPersonMovement.canRun = false;
                yeppy_player.catched = true;
                min_detect.dontmovescreen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

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
    }
}
