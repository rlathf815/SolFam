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

    void OnEnable() // 오브젝트가 활성화될 때마다 실행됨
    {
        Debug.Log("페이드인 시작...");

        // 화면을 어두운 상태에서 시작
        fadeInPanel.color = new Color(0, 0, 0, 1);

        // 페이드인 효과 실행
        StartCoroutine(FadeInFX());
    }

    void Update()
    {
        HP = PlayerStats.Instance.Health;
        Debug.Log("현재 HP=" + HP);
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
        Debug.Log("페이드인 진행 중...");
        float duration = 1.5f; // 1.5초 동안 페이드인
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

        // 최종적으로 화면을 완전히 밝게 설정
        fadeInPanel.color = new Color(0, 0, 0, 0);
        glitch.PixelGlitch = 0f;
        glitch.FrameGlitch = 0f;
        glitch.ChromaticGlitch = 0f;

        Debug.Log("페이드인 완료!");
    }
}
