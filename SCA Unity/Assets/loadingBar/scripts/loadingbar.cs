using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private RectTransform rectComponent;
    private Image imageComp;
    public float speed = 0.1f; // 로딩 속도

    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }

    void Update()
    {
        // `R` 키를 누르고 있을 때만 게이지 증가

            if (imageComp.fillAmount < 1f)
            {
                imageComp.fillAmount += Time.deltaTime * speed;
            }
            else
            {
                Debug.Log("로딩 완료");
                imageComp.fillAmount = 0.0f; // 자동 초기화 (필요 없으면 제거 가능)
            }
        
    }
}
