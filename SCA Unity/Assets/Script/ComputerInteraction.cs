using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ComputerInteraction : MonoBehaviour
{
    public bool hasCertificateFile = false; // �� ��ǻ�Ϳ� �ڰ��� ������ �ִ°�?
    public string certificateType; // �ڰ��� ���� (COSpro, ITQ ��)
    public GameObject screenUI; // ��ǻ�� ȭ�� (������ �� UI)
    public GameObject certificateIcon; // �ڰ��� ���� ������
    public GameObject loadingBarUI; // �ε� �� UI
    public Image loadingBar; // �ε� �� �̹���
    public TextMeshProUGUI progressText; // 1/5 �ڰ��� UI �ؽ�Ʈ
    public GameObject completionMessage; // "000 ��� �Ϸ�" �޽���
    public float loadingSpeed = 1.0f; // �ε� �ӵ�

    private bool isPlayerNear = false; // �÷��̾ ��ó�� �ִ���
    private bool isComputerOn = false; // ��ǻ�Ͱ� ���� �ִ���
    private bool isLoading = false; // ���� �ε� ������
    private float currentLoad = 0f; // �ε� �� ���൵
    private static int acquiredCertificates = 0; // ����� �ڰ��� ����

    void Start()
    {
        // �ڰ��� ������ �ִ� ��ǻ�Ͷ�� ������ Ȱ��ȭ
        if (hasCertificateFile)
        {
            certificateIcon.SetActive(true);
        }
        else
        {
            certificateIcon.SetActive(false);
        }

        // UI �⺻�� ����
        screenUI.SetActive(false);
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(false);
    }

    void Update()
    {
        // �÷��̾ ��ó�� �ְ� 'E'�� ������ ��ǻ�͸� �Ѱ� ��
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ToggleComputer();
        }

        // ��ǻ�Ͱ� ���� �ְ�, �ڰ��� ������ �ִ� ��� 'F' Ű�� ������ �� �� ����
        if (isComputerOn && hasCertificateFile && Input.GetKeyDown(KeyCode.F))
        {
            StartLoading();
        }

        // 'R' Ű�� ������ �ε� ����
        if (isLoading && Input.GetKey(KeyCode.R))
        {
            UpdateLoading();
        }
    }

    void ToggleComputer()
    {
        isComputerOn = !isComputerOn;
        screenUI.SetActive(isComputerOn);
        Debug.Log(isComputerOn ? "��ǻ�� ����" : "��ǻ�� ����");
    }

    void StartLoading()
    {
        if (!isLoading)
        {
            Debug.Log($" {certificateType} ���� ���� ����");
            loadingBarUI.SetActive(true);
            isLoading = true;
            currentLoad = 0f;
            loadingBar.fillAmount = 0f;
        }
    }

    void UpdateLoading()
    {
        if (currentLoad < 1f)
        {
            currentLoad += Time.deltaTime * loadingSpeed;
            loadingBar.fillAmount = currentLoad;
        }
        else
        {
            CompleteLoading();
        }
    }

    void CompleteLoading()
    {
        Debug.Log($" {certificateType} ��� �Ϸ�!");
        isLoading = false;
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(true);
        StartCoroutine(HideCompletionMessage());

        // �ڰ��� ���� ������Ʈ
        acquiredCertificates++;
        progressText.text = $"{acquiredCertificates}/5";
    }

    IEnumerator HideCompletionMessage()
    {
        yield return new WaitForSeconds(2f);
        completionMessage.SetActive(false);
    }

    // �÷��̾ ��ǻ�� ��ó�� �ִ��� ����
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
