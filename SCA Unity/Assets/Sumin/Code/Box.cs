using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms;

public class Box : MonoBehaviour
{
    public int Coffee_I;
    public int MenSeter_I;
    public int Coffee_b;
    public int MenSeter_b;

    public GameObject BoxPanel; // UI �г�
    public GameObject box;
    private bool BoxInRange = false;

    public int point;
    // Start is called before the first frame update
    void Start()
    {
        Renderer jenRenderer = box.GetComponent<Renderer>();
        BoxInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (point == 0)
        {
            Renderer boxState = box.GetComponent<Renderer>();
            boxState.enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player")) // �÷��̾ ������ ��� ��
        {
            BoxInRange = false; // �÷��̾ �������� ����
            BoxPanel.SetActive(false); // �г� ��Ȱ��ȭ
        }
    }
}
