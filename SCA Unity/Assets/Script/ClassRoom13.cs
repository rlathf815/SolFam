using UnityEngine;
using System;
public class ClassRoom13 : MonoBehaviour
{
    //public event Action onPlayerEnter; // �÷��̾� ���� �̺�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�κ� ������!");
            PlayerStats.Instance.TakeDamage(3);
        }
    }
}
