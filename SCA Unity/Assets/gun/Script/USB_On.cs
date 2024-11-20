using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB_On : MonoBehaviour
{
    private bool state; // USB 상태
    private bool isClose; // 플레이어가 가까이 있는지 여부
    private bool canPressE; // E 키를 눌릴 수 있는지 여부
    public GameObject Target; // 활성화할 게임 오브젝트
    public GameObject text; // 텍스트 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        state = false;
        isClose = false;
        canPressE = true; // 초반에 E 키를 눌릴 수 있도록 설정
        text.SetActive(false); // 초기 텍스트 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && canPressE && Input.GetKeyDown(KeyCode.E)) // E 키 누름
        {
            Debug.Log("E " + state);
            state = !state; // 상태를 토글
            Target.SetActive(state); // 상태에 따라 타겟 활성화/비활성화
            Debug.Log("USB state: " + state);

            // 텍스트 비활성화
            text.SetActive(false);

            // E 키를 누른 후 더 이상 누를 수 없도록 설정
            canPressE = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            isClose = true;
            text.SetActive(true); // 플레이어가 가까이 왔을 때 텍스트 활성화
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            isClose = false;
            text.SetActive(false); // 플레이어가 멀어졌을 때 텍스트 비활성화
        }
    }
}