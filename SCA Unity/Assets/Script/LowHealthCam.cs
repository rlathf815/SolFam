using System.Collections;
using UnityEngine;
using Leguar.LowHealth;
using UnityEngine.UI;


public class LowHealthCam : MonoBehaviour
{
    float HP;
    public LowHealthController shaderControllerScript;
    public FastGlitch glitch;
    public Image fadeInPanel;

    void OnEnable() // ������Ʈ�� Ȱ��ȭ�� ������ �����
    {
        Debug.Log("���̵��� ����...");

        // ȭ���� ��ο� ���¿��� ����
        fadeInPanel.color = new Color(0, 0, 0, 1);

        // ���̵��� ȿ�� ����
        StartCoroutine(FadeInFX());
    }

    void Update()
    {
        HP = PlayerStats.Instance.Health;
        Debug.Log("���� HP=" + HP);
        //shaderControllerScript.SetPlayerHealthInstantly(HP / 3f);

        if (HP == 1)
        {
            glitch.PixelGlitch = 0.3f;
            glitch.FrameGlitch = 0.1f;
            glitch.ChromaticGlitch = 0.2f;
            shaderControllerScript.SetPlayerHealthInstantly(0.3f);
        }
        else if (HP == 2)
        {
            glitch.PixelGlitch = 0.06f;
            glitch.ChromaticGlitch = 0.05f;
            shaderControllerScript.SetPlayerHealthInstantly(0.6f);

        }
    }

    private IEnumerator FadeInFX()
    {
        Debug.Log("���̵��� ���� ��...");
        float duration = 1.5f; // 1.5�� ���� ���̵���
        float elapsedTime = 0f;
        Color color = fadeInPanel.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            float glitchEffect = Mathf.Lerp(0.5f, 0f, elapsedTime / duration);

            fadeInPanel.color = new Color(0, 0, 0, alpha);
            glitch.PixelGlitch = glitchEffect;
            glitch.FrameGlitch = glitchEffect;
            glitch.ChromaticGlitch = glitchEffect;

            yield return null;
        }

        // ���������� ȭ���� ������ ��� ����
        fadeInPanel.color = new Color(0, 0, 0, 0);
        glitch.PixelGlitch = 0f;
        glitch.FrameGlitch = 0f;
        glitch.ChromaticGlitch = 0f;

        Debug.Log("���̵��� �Ϸ�!");
    }
}
