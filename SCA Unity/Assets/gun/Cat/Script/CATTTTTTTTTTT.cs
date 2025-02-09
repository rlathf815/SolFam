using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CATTTTTTTTTTT : MonoBehaviour
{
    public GameObject text;
    public Transform player; // 플레이어의 Transform
    public float disappearTime = 5f; // 사라지는 시간
    public GameObject cat;
    public Animator controller;
    public float followDistance = 1.5f; //플레이어랑 고양이 사이 간격

    private bool isFollowing = false; // 현재 따라오고 있는지 여부
    private Renderer objectRenderer; // 오브젝트의 Renderer
    private bool isClose;

    //public AudioClip soundClip; // 재생할 사운드 클립
    public AudioSource audioSource; // 오디오 소스 컴포넌트
    void Start()
    {
        isClose = false;
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = true; // 처음에는 보이도록 설정
        //controller = GetComponent<Animator>();

        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.clip = soundClip;
    }

    void Update()
    {
        // E 키를 눌렀을 때
        if (isClose && Input.GetKeyDown(KeyCode.E))
        {
            // isFollowing이 true일 경우 반응하지 않음
            if (isFollowing)
            {
                return; // 아무 반응도 하지 않음
            }

            // 30% 확률로 따라오기
            if (Random.value <= 0.98f) // 30% 확률
            {
                isFollowing = true;
                controller.SetTrigger("Follow");
                Debug.Log("Following the player.");
                audioSource.Play();
            }
            else
            {
                isFollowing = false;
                Debug.Log("Not following. Starting disappear coroutine.");
                StartCoroutine(DisappearAndReappear());
                return;
            }
        }

        // 따라오기
        if (isFollowing)
        {
            text.SetActive(false);
            Follow();
        }
    }

    void Follow()
    {
        Vector3 targetPosition = player.position - player.forward * followDistance;//목표 지점(플레이어위치 + 간격)을 미리 계산
        // 플레이어의 위치로 부드럽게 이동
        
        // 플레이어의 속도를 체크하여 animator의 isWalking 파라미터 업데이트
        if (player.GetComponent<Rigidbody>().velocity.magnitude > 0.1f) // 플레이어가 움직일 때
        {
            // 플레이어를 바라보도록 회전
            Vector3 direction = (player.position - cat.transform.position).normalized; // 플레이어 방향 벡터
            if (direction != Vector3.zero) // 방향 벡터가 0이 아닐 때만 회전
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction); // 방향 벡터를 기반으로 회전 생성
                cat.transform.rotation = Quaternion.Slerp(cat.transform.rotation, lookRotation, Time.deltaTime * 5f); // 부드럽게 회전
            }
            controller.SetBool("isWalking", true);
        }
        else // 플레이어가 멈출 때
        {
            controller.SetBool("isWalking", false);
            cat.transform.position = cat.transform.position;
        }
    }



    private IEnumerator DisappearAndReappear()
    {
        // 오브젝트를 사라지게 함
        Debug.Log("Disappearing...");
        objectRenderer.enabled = false;
        yield return new WaitForSeconds(disappearTime);
        // 오브젝트를 다시 나타나게 함
        Debug.Log("Reappearing...");
        objectRenderer.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isClose = true;
            text.SetActive(true);
            Debug.Log("trigger entered");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isClose = false;
            text.SetActive(false);
            Debug.Log("trigger exit");
        }
    }
}