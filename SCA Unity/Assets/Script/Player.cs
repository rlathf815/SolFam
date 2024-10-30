using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton instance
    public static Player Instance { get; private set; }

    public GameObject LetterE;

    private void Awake()
    {
        // Singleton ������ ���� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (LetterE != null)
        {
            LetterE.SetActive(false); // �ʱ⿡�� LetterF�� ��Ȱ��ȭ�ǵ��� ����
        }
    }
}
