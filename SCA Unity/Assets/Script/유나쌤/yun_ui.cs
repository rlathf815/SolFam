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
                        moon.text = "  컴퓨터의 용량을 늘리는 올바른 방법은?";
                        dab1.text = "운영체제 삭제";
                        dab2.text = "쓸모없는 파일 정리";
                        dab3.text = "컴퓨터 한대 치기";
                        break;
                    case 1:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  컴퓨터 바이러스를 치료하는 올바른 방법은?";
                        dab1.text = "기도하기";
                        dab2.text = "백신을 사용";
                        dab3.text = "물에 담근다";
                        break;
                    case 2:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  문자를 복사하는 올바른 방법은?";
                        dab1.text = "Ctrl+C";
                        dab2.text = "사진기를 사용";
                        dab3.text = "손으로 옮긴다";
                        break;
                    case 3:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  컴퓨터가 갑자기 멈췄을때";
                        dab1.text = "한대 때린다";
                        dab2.text = "새로 산다";
                        dab3.text = "껐다 킨다";
                        break;
                    case 4:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  마우스를 움직이는 올바른 방법은?";
                        dab1.text = "손을 이용한다";
                        dab2.text = "염력을 깨우친다";
                        dab3.text = "기다린다";
                        break;
                    case 5:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  와이파이를 연결하는 방법";
                        dab1.text = "안테나를 만든다";
                        dab2.text = "애원한다";
                        dab3.text = "연결버튼을 누른다";
                        break;

                }
                canvas.SetActive(true); //UI 보이게 하기
            }
        }
    }
}
