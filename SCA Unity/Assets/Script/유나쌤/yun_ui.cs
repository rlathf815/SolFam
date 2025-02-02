using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Properties;
using UnityEngine;

public class yun_ui : MonoBehaviour
{
    public Transform player;
    public GameObject canvas;
    public TextMeshProUGUI moon;
    public TextMeshProUGUI dab1;
    public TextMeshProUGUI dab2;
    public TextMeshProUGUI dab3;
    public static bool openui=false;
    public static bool hasopenui = false;
    void Update()
    {
        if (openui==true)
        {
            if (hasopenui == false)
            {
                hasopenui = true;
                switch (Random.Range(0, 6)){
                    case 0:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ��ǻ���� �뷮�� �ø��� �ùٸ� �����?";
                        dab1.text = "�ü�� ����";
                        dab2.text = "������� ���� ����";
                        dab3.text = "��ǻ�� �Ѵ� ġ��";
                        break;
                    case 1:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ��ǻ�� ���̷����� ġ���ϴ� �ùٸ� �����?";
                        dab1.text = "�⵵�ϱ�";
                        dab2.text = "����� ���";
                        dab3.text = "���� ��ٴ�";
                        break;
                    case 2:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ���ڸ� �����ϴ� �ùٸ� �����?";
                        dab1.text = "Ctrl+C";
                        dab2.text = "�����⸦ ���";
                        dab3.text = "������ �ű��";
                        break;
                    case 3:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  ��ǻ�Ͱ� ���ڱ� ��������";
                        dab1.text = "�Ѵ� ������";
                        dab2.text = "���� ���";
                        dab3.text = "���� Ų��";
                        break;
                    case 4:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ���콺�� �����̴� �ùٸ� �����?";
                        dab1.text = "���� �̿��Ѵ�";
                        dab2.text = "������ ����ģ��";
                        dab3.text = "��ٸ���";
                        break;
                    case 5:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  �������̸� �����ϴ� ���";
                        dab1.text = "���׳��� �����";
                        dab2.text = "�ֿ��Ѵ�";
                        dab3.text = "�����ư�� ������";
                        break;

                }
                canvas.SetActive(true); //UI ���̰� �ϱ�
            }
        }
    }
}
