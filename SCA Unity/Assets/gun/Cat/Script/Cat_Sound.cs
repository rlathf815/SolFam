using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_ : MonoBehaviour
{
    //public AudioClip soundClip; // ����� ���� Ŭ��
    public AudioSource audioSource; // ����� �ҽ� ������Ʈ
    public float maximumRange=60f;
    public float minimumRange=1f;

    void Start()
    {
        // AudioSource ������Ʈ�� �߰��ϰ� �����ɴϴ�.
        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.clip = soundClip;

        // �ڷ�ƾ ����
        StartCoroutine(PlaySoundAfterRandomTime());
    }

    private System.Collections.IEnumerator PlaySoundAfterRandomTime()
    {
        // 180�ʿ��� 240�� ������ ���� �ð� ����
        float randomTime = Random.Range(minimumRange,maximumRange);

        // ���� �ð� ���
        yield return new WaitForSeconds(randomTime);

        // ���� ���
        audioSource.Play();
    }
}
