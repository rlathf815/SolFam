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
                if (Random.Range(0, 2) == 1)//랜덤으로 문제 내기
                {
                    moon.text = "  컴퓨터의 용량을 늘리는 올바른 방법은?";
                    dab1.text = "운영체제 삭제";
                    dab2.text = "쓸모없는 파일 정리";
                    dab3.text = "컴퓨터 한대 치기";
                }
                else
                {
                    moon.text = "  컴퓨터 바이러스를 치료하는 올바른 방법은?";
                    dab1.text = "기도하기";
                    dab2.text = "백신을 사용";
                    dab3.text = "물에 담근다";
                }
                canvas.SetActive(true); //UI 보이게 하기
            }
        }
    }
}
