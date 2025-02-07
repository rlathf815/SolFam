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
    private static ComputerInteraction activeComputer = null; // ���� ���� �ִ� ��ǻ�� (�ϳ��� �������� ����)

    public GameObject Etext; //"press E to turn on"

    public AudioSource click;
    public AudioSource load;
    public AudioSource complete;

    private bool isPlayingLoadSound = false; // �ε� ���尡 ��� ������ Ȯ��

    void Start()
    {
        StartCoroutine(wait(2f));

        // UI �⺻�� ����
        screenUI.SetActive(false);
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(false);
        loadingBar.fillAmount = 0f;
    }

    void Update()
    {
        if (isPlayerNear && (!isComputerOn || activeComputer == this))
        {
            Etext.SetActive(true);
        }
        else
        {
            Etext.SetActive(false);
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            click.Play();
            ToggleComputer();
        }

        if (isComputerOn && hasCertificateFile && Input.GetKeyDown(KeyCode.F))
        {
            click.Play();
            StartLoading();
        }

        if (isLoading && Input.GetKey(KeyCode.R))
        {
            if (!isPlayingLoadSound)
            {
                load.Play();
                isPlayingLoadSound = true;
            }
            UpdateLoading();
        }

        if (isLoading && Input.GetKeyUp(KeyCode.R))
        {
            load.Stop();
            isPlayingLoadSound = false;
        }
    }

    void ToggleComputer()
    {
        if (!isComputerOn)
        {
            if (activeComputer != null && activeComputer != this)
            {
                activeComputer.ForceTurnOff();
            }

            isComputerOn = true;
            activeComputer = this;
            screenUI.SetActive(true);
            Debug.Log("��ǻ�� ����");
        }
        else
        {
            isComputerOn = false;
            activeComputer = null;
            screenUI.SetActive(false);
            Debug.Log("��ǻ�� ����");
        }
    }

    public void ForceTurnOff()
    {
        isComputerOn = false;
        screenUI.SetActive(false);
    }

    void StartLoading()
    {
        if (!isLoading && hasCertificateFile)
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
        load.Stop();
        isPlayingLoadSound = false;

        Debug.Log($"{certificateType} ��� �Ϸ�!");
        isLoading = false;
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(true);
        StartCoroutine(HideCompletionMessage());
        complete.Play();

        acquiredCertificates++;
        progressText.text = $"{acquiredCertificates}/5";

        hasCertificateFile = false;
        certificateIcon.SetActive(false);
    }

    IEnumerator HideCompletionMessage()
    {
        yield return new WaitForSeconds(2f);
        completionMessage.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("player trigger on");
        }
    }

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