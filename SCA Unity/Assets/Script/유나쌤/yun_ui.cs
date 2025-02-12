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
                switch (Random.Range(0, 20)) // 문제 개수를 20개로 확대
                {
                    case 0:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  컴퓨터의 용량을 늘리는 올바른 방법은?";
                        dab1.text = "운영체제 삭제";
                        dab2.text = "불필요한 파일 정리";
                        dab3.text = "컴퓨터 본체를 흔든다";
                        break;
                    case 1:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  컴퓨터 바이러스를 치료하는 올바른 방법은?";
                        dab1.text = "기도하기";
                        dab2.text = "백신 프로그램 실행";
                        dab3.text = "하드디스크를 부수기";
                        break;
                    case 2:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  문자를 복사하는 단축키는?";
                        dab1.text = "Ctrl+C";
                        dab2.text = "Ctrl+V";
                        dab3.text = "Shift+C";
                        break;
                    case 3:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  컴퓨터가 갑자기 멈췄을 때 올바른 대처법은?";
                        dab1.text = "컴퓨터를 발로 찬다";
                        dab2.text = "새로 산다";
                        dab3.text = "강제 종료 후 다시 켠다";
                        break;
                    case 4:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  마우스를 움직이는 올바른 방법은?";
                        dab1.text = "손을 이용한다";
                        dab2.text = "염력을 이용한다";
                        dab3.text = "기다린다";
                        break;
                    case 5:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  와이파이를 연결하는 방법은?";
                        dab1.text = "안테나를 만든다";
                        dab2.text = "애원한다";
                        dab3.text = "Wi-Fi 목록에서 선택 후 연결한다";
                        break;
                    case 6:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  파이썬에서 변수를 선언하는 올바른 방법은?";
                        dab1.text = "x = 10";
                        dab2.text = "10 -> x";
                        dab3.text = "변수 x는 10이다";
                        break;
                    case 7:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  ITQ 한글에서 표를 삽입하는 방법은?";
                        dab1.text = "Alt + T";
                        dab2.text = "Ctrl + P";
                        dab3.text = "Ctrl + N, T";
                        break;
                    case 8:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  정보처리기능사에서 프로그래밍 언어가 아닌 것은?";
                        dab1.text = "C언어";
                        dab2.text = "HTML";
                        dab3.text = "Java";
                        break;
                    case 9:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  파이썬에서 리스트의 길이를 구하는 함수는?";
                        dab1.text = "size()";
                        dab2.text = "count()";
                        dab3.text = "len()";
                        break;
                    case 10:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  정보처리기능사에서 CPU의 역할은?";
                        dab1.text = "연산 및 제어";
                        dab2.text = "전기 공급";
                        dab3.text = "문서 저장";
                        break;
                    case 11:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  파이썬에서 반복문을 종료하는 키워드는?";
                        dab1.text = "continue";
                        dab2.text = "break";
                        dab3.text = "pass";
                        break;
                    case 12:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  RAM의 역할은?";
                        dab1.text = "데이터 영구 저장";
                        dab2.text = "전원을 끈 후에도 데이터 유지";
                        dab3.text = "작업 속도 향상을 위한 임시 저장";
                        break;
                    case 13:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  파이썬에서 딕셔너리의 값을 가져오는 메서드는?";
                        dab1.text = "get()";
                        dab2.text = "fetch()";
                        dab3.text = "retrieve()";
                        break;
                    case 14:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  파이썬에서 한 줄 주석을 다는 방법은?";
                        dab1.text = "// 주석";
                        dab2.text = "# 주석";
                        dab3.text = "/* 주석 */";
                        break;
                    case 15:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  정보처리기능사에서 논리 연산자가 아닌 것은?";
                        dab1.text = "AND";
                        dab2.text = "OR";
                        dab3.text = "ADD";
                        break;
                    case 16:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  파이썬에서 리스트에 요소를 추가하는 메서드는?";
                        dab1.text = "append()";
                        dab2.text = "add()";
                        dab3.text = "insert()";
                        break;
                    case 17:
                        Yun_button.moonjaeDab = 2;
                        moon.text = "  정보처리기능사에서 데이터베이스에서 데이터를 가져오는 언어는?";
                        dab1.text = "HTML";
                        dab2.text = "SQL";
                        dab3.text = "CSS";
                        break;
                    case 18:
                        Yun_button.moonjaeDab = 3;
                        moon.text = "  파이썬에서 조건문을 작성할 때 사용하는 키워드는?";
                        dab1.text = "case";
                        dab2.text = "switch";
                        dab3.text = "if";
                        break;
                    case 19:
                        Yun_button.moonjaeDab = 1;
                        moon.text = "  정보처리기능사에서 OS의 역할이 아닌 것은?";
                        dab1.text = "데이터 저장";
                        dab2.text = "프로세스 관리";
                        dab3.text = "파일 시스템 관리";
                        break;
                }
                canvas.SetActive(true); // UI 보이게 하기
            }
        }
    }
}
