using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    [SerializeField] private Image cooldownOverlay; // 쿨다운을 표시할 오버레이 (반투명 원)
    [SerializeField] private Text cooldownText; // 남은 시간을 표시할 텍스트

    private bool isCooldown = false;
    private float cooldownDuration;
    private float cooldownStartTime;

    private void Start()
    {
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0f; // 처음에는 쿨다운이 없도록 설정
        }
        if (cooldownText != null)
        {
            cooldownText.text = "";
        }
        //StartCooldown(5f);
    }

    private void Update()
    {
        if (isCooldown)
        {
            UpdateCooldown();
        }
    }

    /// <summary>
    /// 쿨다운 시작 메서드 (외부에서 호출 가능)
    /// </summary>
    /// <param name="duration">쿨다운 지속 시간 (초)</param>
    public void StartCooldown(float duration)
    {
        if (!isCooldown && duration > 0f)
        {
            cooldownDuration = duration;
            cooldownStartTime = Time.time;
            isCooldown = true;

            if (cooldownOverlay != null)
            {
                cooldownOverlay.fillAmount = 1f;
            }
        }
    }

    /// <summary>
    /// 쿨다운 진행 업데이트
    /// </summary>
    private void UpdateCooldown()
    {
        float elapsed = Time.time - cooldownStartTime;
        float remaining = cooldownDuration - elapsed;

        if (remaining > 0)
        {
            if (cooldownText != null)
            {
                cooldownText.text = remaining.ToString("F1"); // 소수점 한 자리까지 표시
            }

            if (cooldownOverlay != null)
            {
                cooldownOverlay.fillAmount = Mathf.Clamp01(remaining / cooldownDuration);
            }
        }
        else
        {
            isCooldown = false;

            if (cooldownText != null)
            {
                cooldownText.text = "";
            }

            if (cooldownOverlay != null)
            {
                cooldownOverlay.fillAmount = 0f;
            }
        }
    }
}
