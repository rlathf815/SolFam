using UnityEngine;
using System;

public class LobbyTrigger : MonoBehaviour
{
    public event Action onPlayerEnter; // �÷��̾� ���� �̺�Ʈ
    private bool isPlayerInLobby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�κ� ������!");
            isPlayerInLobby = true; // �÷��̾ �κ� �������� ���
            onPlayerEnter?.Invoke(); // �̺�Ʈ �߻� (������ ��� �Լ� ����)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�κ񿡼� ����!");
            isPlayerInLobby = false; // �÷��̾ �κ� �������� ���
        }
    }

    public bool IsPlayerInLobby()
    {
        return isPlayerInLobby;
    }
}
