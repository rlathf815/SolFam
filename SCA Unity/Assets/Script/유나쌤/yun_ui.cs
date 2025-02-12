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
    public static bool openui = false;
    public static bool hasopenui = false;

    void Update()
    {
        if (openui == true)
        {
            if (hasopenui == false)
            {
                hasopenui = true;
                switch (Random.Range(0, 20)) // ���� ������ 20���� Ȯ��
                {
                    case 0:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ��ǻ���� �뷮�� �ø��� �ùٸ� �����?";
                        dab1.text = "�ü�� ����";
                        dab2.text = "���ʿ��� ���� ����";
                        dab3.text = "��ǻ�� ��ü�� ����";
                        break;
                    case 1:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ��ǻ�� ���̷����� ġ���ϴ� �ùٸ� �����?";
                        dab1.text = "�⵵�ϱ�";
                        dab2.text = "��� ���α׷� ����";
                        dab3.text = "�ϵ��ũ�� �μ���";
                        break;
                    case 2:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ���ڸ� �����ϴ� ����Ű��?";
                        dab1.text = "Ctrl+C";
                        dab2.text = "Ctrl+V";
                        dab3.text = "Shift+C";
                        break;
                    case 3:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  ��ǻ�Ͱ� ���ڱ� ������ �� �ùٸ� ��ó����?";
                        dab1.text = "��ǻ�͸� �߷� ����";
                        dab2.text = "���� ���";
                        dab3.text = "���� ���� �� �ٽ� �Ҵ�";
                        break;
                    case 4:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ���콺�� �����̴� �ùٸ� �����?";
                        dab1.text = "���� �̿��Ѵ�";
                        dab2.text = "������ �̿��Ѵ�";
                        dab3.text = "��ٸ���";
                        break;
                    case 5:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  �������̸� �����ϴ� �����?";
                        dab1.text = "���׳��� �����";
                        dab2.text = "�ֿ��Ѵ�";
                        dab3.text = "Wi-Fi ��Ͽ��� ���� �� �����Ѵ�";
                        break;
                    case 6:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ���̽㿡�� ������ �����ϴ� �ùٸ� �����?";
                        dab1.text = "x = 10";
                        dab2.text = "10 -> x";
                        dab3.text = "���� x�� 10�̴�";
                        break;
                    case 7:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  ITQ �ѱۿ��� ǥ�� �����ϴ� �����?";
                        dab1.text = "Alt + T";
                        dab2.text = "Ctrl + P";
                        dab3.text = "Ctrl + N, T";
                        break;
                    case 8:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ����ó����ɻ翡�� ���α׷��� �� �ƴ� ����?";
                        dab1.text = "C���";
                        dab2.text = "HTML";
                        dab3.text = "Java";
                        break;
                    case 9:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  ���̽㿡�� ����Ʈ�� ���̸� ���ϴ� �Լ���?";
                        dab1.text = "size()";
                        dab2.text = "count()";
                        dab3.text = "len()";
                        break;
                    case 10:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ����ó����ɻ翡�� CPU�� ������?";
                        dab1.text = "���� �� ����";
                        dab2.text = "���� ����";
                        dab3.text = "���� ����";
                        break;
                    case 11:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ���̽㿡�� �ݺ����� �����ϴ� Ű�����?";
                        dab1.text = "continue";
                        dab2.text = "break";
                        dab3.text = "pass";
                        break;
                    case 12:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  RAM�� ������?";
                        dab1.text = "������ ���� ����";
                        dab2.text = "������ �� �Ŀ��� ������ ����";
                        dab3.text = "�۾� �ӵ� ����� ���� �ӽ� ����";
                        break;
                    case 13:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ���̽㿡�� ��ųʸ��� ���� �������� �޼����?";
                        dab1.text = "get()";
                        dab2.text = "fetch()";
                        dab3.text = "retrieve()";
                        break;
                    case 14:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ���̽㿡�� �� �� �ּ��� �ٴ� �����?";
                        dab1.text = "// �ּ�";
                        dab2.text = "# �ּ�";
                        dab3.text = "/* �ּ� */";
                        break;
                    case 15:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  ����ó����ɻ翡�� �� �����ڰ� �ƴ� ����?";
                        dab1.text = "AND";
                        dab2.text = "OR";
                        dab3.text = "ADD";
                        break;
                    case 16:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ���̽㿡�� ����Ʈ�� ��Ҹ� �߰��ϴ� �޼����?";
                        dab1.text = "append()";
                        dab2.text = "add()";
                        dab3.text = "insert()";
                        break;
                    case 17:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  ����ó����ɻ翡�� �����ͺ��̽����� �����͸� �������� ����?";
                        dab1.text = "HTML";
                        dab2.text = "SQL";
                        dab3.text = "CSS";
                        break;
                    case 18:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  ���̽㿡�� ���ǹ��� �ۼ��� �� ����ϴ� Ű�����?";
                        dab1.text = "case";
                        dab2.text = "switch";
                        dab3.text = "if";
                        break;
                    case 19:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  ����ó����ɻ翡�� OS�� ������ �ƴ� ����?";
                        dab1.text = "������ ����";
                        dab2.text = "���μ��� ����";
                        dab3.text = "���� �ý��� ����";
                        break;
                }
                canvas.SetActive(true); // UI ���̰� �ϱ�
            }
        }
    }
}
