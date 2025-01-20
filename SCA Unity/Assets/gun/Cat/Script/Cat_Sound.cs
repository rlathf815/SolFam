using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_ : MonoBehaviour
{
    //public AudioClip soundClip; // 재생할 사운드 클립
    public AudioSource audioSource; // 오디오 소스 컴포넌트
    public float maximumRange=60f;
    public float minimumRange=1f;

    void Start()
    {
        // AudioSource 컴포넌트를 추가하고 가져옵니다.
        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.clip = soundClip;

        // 코루틴 시작
        StartCoroutine(PlaySoundAfterRandomTime());
    }

    private System.Collections.IEnumerator PlaySoundAfterRandomTime()
    {
        // 180초에서 240초 사이의 랜덤 시간 생성
        float randomTime = Random.Range(minimumRange,maximumRange);

        // 랜덤 시간 대기
        yield return new WaitForSeconds(randomTime);

        // 사운드 재생
        audioSource.Play();
    }
}
