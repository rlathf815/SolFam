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
            VirusGUI.PopupVariables vars = new VirusGUI.PopupVariables("��Ģ", "13���ǽ��� �������� �ʽ��ϴ�.", gameObject, "OpenDoor", "", "CancelWindow", "");
            VirusGUI.MultiWindow multi = new VirusGUI.MultiWindow(50, 20, vars);
            multi.popupVariables.runFromCursor = true;
            Debug.Log("�κ� ������!");
            PlayerStats.Instance.TakeDamage(3);
        }
    }
}
