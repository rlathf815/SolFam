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
                if (Random.Range(0, 2) == 1)//�������� ���� ����
                {
                    moon.text = "  ��ǻ���� �뷮�� �ø��� �ùٸ� �����?";
                    dab1.text = "�ü�� ����";
                    dab2.text = "������� ���� ����";
                    dab3.text = "��ǻ�� �Ѵ� ġ��";
                }
                else
                {
                    moon.text = "  ��ǻ�� ���̷����� ġ���ϴ� �ùٸ� �����?";
                    dab1.text = "�⵵�ϱ�";
                    dab2.text = "����� ���";
                    dab3.text = "���� ��ٴ�";
                }
                canvas.SetActive(true); //UI ���̰� �ϱ�
            }
        }
    }
}
