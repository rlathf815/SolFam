using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    [SerializeField] private Image cooldownOverlay; // ��ٿ��� ǥ���� �������� (������ ��)
    [SerializeField] private Text cooldownText; // ���� �ð��� ǥ���� �ؽ�Ʈ

    private bool isCooldown = false;
    private float cooldownDuration;
    private float cooldownStartTime;

    private void Start()
    {
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0f; // ó������ ��ٿ��� ������ ����
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
    /// ��ٿ� ���� �޼��� (�ܺο��� ȣ�� ����)
    /// </summary>
    /// <param name="duration">��ٿ� ���� �ð� (��)</param>
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
    /// ��ٿ� ���� ������Ʈ
    /// </summary>
    private void UpdateCooldown()
    {
        float elapsed = Time.time - cooldownStartTime;
        float remaining = cooldownDuration - elapsed;

        if (remaining > 0)
        {
            if (cooldownText != null)
            {
                cooldownText.text = remaining.ToString("F1"); // �Ҽ��� �� �ڸ����� ǥ��
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
