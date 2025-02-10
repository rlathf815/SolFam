using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFinder : MonoBehaviour
{
    void Start()
    {
        FindCanvases();
    }

    void FindCanvases()
    {
        // 모든 Canvas 오브젝트를 찾습니다.
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            // Render Mode가 Screen Space - Camera인지 확인합니다.
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                // 해당 Canvas의 이름을 로그에 출력합니다.
                Debug.Log("Found Canvas with Screen Space - Camera: " + canvas.name);
            }
        }
    }
}