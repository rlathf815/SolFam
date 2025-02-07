using UnityEngine;
using System;
public class ClassRoom13 : MonoBehaviour
{
    //public event Action onPlayerEnter; // 플레이어 감지 이벤트

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
            Debug.Log("로비 감지됨!");
            PlayerStats.Instance.TakeDamage(3);
        }
    }
}
