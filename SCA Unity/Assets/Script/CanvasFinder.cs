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
        // ��� Canvas ������Ʈ�� ã���ϴ�.
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            // Render Mode�� Screen Space - Camera���� Ȯ���մϴ�.
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                // �ش� Canvas�� �̸��� �α׿� ����մϴ�.
                Debug.Log("Found Canvas with Screen Space - Camera: " + canvas.name);
            }
        }
    }
}