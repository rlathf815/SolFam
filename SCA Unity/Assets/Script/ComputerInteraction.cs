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
    public Text progressText; // 1/5 �ڰ��� UI �ؽ�Ʈ
    public GameObject completionMessage; // "000 ��� �Ϸ�" �޽���
    public float loadingSpeed = 1.0f; // �ε� �ӵ�

    private bool isPlayerNear = false; // �÷��̾ ��ó�� �ִ���
    private bool isComputerOn = false; // ��ǻ�Ͱ� ���� �ִ���
    private bool isLoading = false; // ���� �ε� ������
    private float currentLoad = 0f; // �ε� �� ���൵
    private static int acquiredCertificates = 0; // ����� �ڰ��� ����
    public GameObject Etext; //"press E to turn on"

    public AudioSource click;
    public AudioSource load;
    public AudioSource complete;

    private bool isPlayingLoadSound = false; // �ε� ���尡 ��� ������ Ȯ��

    void Start()
    {
        // �ڰ��� ������ �ִ� ��ǻ�Ͷ�� ������ Ȱ��ȭ
        StartCoroutine(wait(2f));
        
        // UI �⺻�� ����
        screenUI.SetActive(false);
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(false);
        loadingBar.fillAmount = 0f;
    }

    void Update()
    {
        if (isPlayerNear && !isComputerOn)
        {
            Etext.SetActive(true);
        }
        else
        {
            Etext.SetActive(false);
        }

        // �÷��̾ ��ó�� �ְ� 'E'�� ������ ��ǻ�͸� �Ѱ� ��
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            click.Play();
            ToggleComputer();
        }

        // ��ǻ�Ͱ� ���� �ְ�, �ڰ��� ������ �ִ� ��� 'F' Ű�� ������ �� �� ����
        if (isComputerOn && hasCertificateFile && Input.GetKeyDown(KeyCode.F))
        {
            click.Play();
            StartLoading();
        }

        // 'R' Ű�� ������ �ε� ���� & ���� ���
        if (isLoading && Input.GetKey(KeyCode.R))
        {
            if (!isPlayingLoadSound) // �ε� ���尡 �̹� ��� ���� �ƴ� ���� ����
            {
                load.Play();
                isPlayingLoadSound = true;
            }
            UpdateLoading();
        }

        // 'R' Ű���� ���� ���� �ε� ���� ����
        if (isLoading && Input.GetKeyUp(KeyCode.R))
        {
            load.Stop();
            isPlayingLoadSound = false;
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
            Debug.Log(" �ε� �Ϸ�");
        }
    }

    void CompleteLoading()
    {
        load.Stop(); // �ε� �Ϸ� �� �ε� ���� ����
        isPlayingLoadSound = false;

        Debug.Log($"{certificateType} ��� �Ϸ�!");
        isLoading = false;
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(true);
        StartCoroutine(HideCompletionMessage());
        complete.Play();
        // �ڰ��� ���� ������Ʈ
        acquiredCertificates++;
        Debug.Log(acquiredCertificates);
        progressText.text = $"{acquiredCertificates}/5";
    }

    IEnumerator HideCompletionMessage()
    {
        yield return new WaitForSeconds(2f);
        completionMessage.SetActive(false);
    }

    //  �÷��̾ ��ǻ�� ��ó�� ���� ��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("player trigger on");
        }
    }

    //  �÷��̾ ��ǻ�Ϳ��� �־��� ��
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("player trigger off");
        }
    }
    private IEnumerator wait(float sec)
    {
     
        yield return new WaitForSeconds(sec);
        Debug.Log(hasCertificateFile);
        certificateIcon.SetActive(hasCertificateFile);
        if (hasCertificateFile) Debug.Log(certificateType);

    }
}
