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
            VirusGUI.PopupVariables vars = new VirusGUI.PopupVariables("규칙", "13강의실은 존재하지 않습니다.", gameObject, "OpenDoor", "", "CancelWindow", "");
            VirusGUI.MultiWindow multi = new VirusGUI.MultiWindow(50, 20, vars);
            multi.popupVariables.runFromCursor = true;
            Debug.Log("로비 감지됨!");
            PlayerStats.Instance.TakeDamage(3);
        }
    }
}
