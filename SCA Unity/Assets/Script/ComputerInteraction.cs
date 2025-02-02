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
    public TextMeshProUGUI progressText; // 1/5 자격증 UI 텍스트
    public GameObject completionMessage; // "000 취득 완료" 메시지
    public float loadingSpeed = 1.0f; // 로딩 속도

    private bool isPlayerNear = false; // 플레이어가 근처에 있는지
    private bool isComputerOn = false; // 컴퓨터가 켜져 있는지
    private bool isLoading = false; // 현재 로딩 중인지
    private float currentLoad = 0f; // 로딩 바 진행도
    private static int acquiredCertificates = 0; // 취득한 자격증 개수

    void Start()
    {
        // 자격증 파일이 있는 컴퓨터라면 아이콘 활성화
        if (hasCertificateFile)
        {
            certificateIcon.SetActive(true);
        }
        else
        {
            certificateIcon.SetActive(false);
        }

        // UI 기본값 설정
        screenUI.SetActive(false);
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(false);
    }

    void Update()
    {
        // 플레이어가 근처에 있고 'E'를 누르면 컴퓨터를 켜고 끔
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ToggleComputer();
        }

        // 컴퓨터가 켜져 있고, 자격증 파일이 있는 경우 'F' 키로 파일을 열 수 있음
        if (isComputerOn && hasCertificateFile && Input.GetKeyDown(KeyCode.F))
        {
            StartLoading();
        }

        // 'R' 키를 누르면 로딩 진행
        if (isLoading && Input.GetKey(KeyCode.R))
        {
            UpdateLoading();
        }
    }

    void ToggleComputer()
    {
        isComputerOn = !isComputerOn;
        screenUI.SetActive(isComputerOn);
        Debug.Log(isComputerOn ? "컴퓨터 켜짐" : "컴퓨터 꺼짐");
    }

    void StartLoading()
    {
        if (!isLoading)
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
        }
    }

    void CompleteLoading()
    {
        Debug.Log($" {certificateType} 취득 완료!");
        isLoading = false;
        loadingBarUI.SetActive(false);
        completionMessage.SetActive(true);
        StartCoroutine(HideCompletionMessage());

        // 자격증 개수 업데이트
        acquiredCertificates++;
        progressText.text = $"{acquiredCertificates}/5";
    }

    IEnumerator HideCompletionMessage()
    {
        yield return new WaitForSeconds(2f);
        completionMessage.SetActive(false);
    }

    // 플레이어가 컴퓨터 근처에 있는지 감지
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
