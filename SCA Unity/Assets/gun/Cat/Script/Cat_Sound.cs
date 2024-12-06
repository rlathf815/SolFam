using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_ : MonoBehaviour
{
    public AudioClip soundClip; // ����� ���� Ŭ��
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ

    void Start()
    {
        // AudioSource ������Ʈ�� �߰��ϰ� �����ɴϴ�.
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;

        // �ڷ�ƾ ����
        StartCoroutine(PlaySoundAfterRandomTime());
    }

    private System.Collections.IEnumerator PlaySoundAfterRandomTime()
    {
        // 180�ʿ��� 300�� ������ ���� �ð� ����
        float randomTime = Random.Range(180f, 240);

        // ���� �ð� ���
        yield return new WaitForSeconds(randomTime);

        // ���� ���
        audioSource.Play();
    }
}
