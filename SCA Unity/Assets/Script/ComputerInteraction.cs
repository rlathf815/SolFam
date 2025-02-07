using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerInteraction : MonoBehaviour
{
    public bool hasCertificateFile = false; // 이 컴퓨터에 자격증 파일이 있는가?
    public string certificateType; // 자격증 종류 (COSpro, ITQ 등)
    public GameObject screenUI; // 컴퓨터 화면 (켜졌을 때 UI)
    public GameObject certificateIcon; // 자격증 파일 아이콘
    public GameObject loadingBarUI; // 로딩 바 UI
    public Image loadingBar; // 로딩 바 이미지
    public Text progressText; // 1/5 자격증 UI 텍스트
    public GameObject completionMessage; // "000 취득 완료" 메시지
    public float loadingSpeed = 1.0f; // 로딩 속도

    private bool isPlayerNear = false; // 플레이어가 근처에 있는지
    private bool isComputerOn = false; // 컴퓨터가 켜져 있는지
    private bool isLoading = false; // 현재 로딩 중인지
    private float currentLoad = 0f; // 로딩 바 진행도
    private static int acquiredCertificates = 0; // 취득한 자격증 개수
    private static ComputerInteraction activeComputer = null; // 현재 켜져 있는 컴퓨터 (하나만 켜지도록 관리)

    public GameObject Etext; //"press E to turn on"

    public AudioSource click;
    public AudioSource load;
    public AudioSource complete;

    private bool isPlayingLoadSound = false; // 로딩 사운드가 재생 중인지 확인

    void Start()
    {
        StartCoroutine(wait(2f));

        // UI 기본값 설정
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
            Debug.Log("컴퓨터 켜짐");
        }
        else
        {
            isComputerOn = false;
            activeComputer = null;
            screenUI.SetActive(false);
            Debug.Log("컴퓨터 꺼짐");
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
            Debug.Log($" {certificateType} 파일 열기 시작");
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
            Debug.Log(" 로딩 완료");
        }
    }

    void CompleteLoading()
    {
        load.Stop();
        isPlayingLoadSound = false;

        Debug.Log($"{certificateType} 취득 완료!");
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