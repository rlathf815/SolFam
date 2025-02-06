using UnityEngine;
using System;

public class LobbyTrigger : MonoBehaviour
{
    public event Action onPlayerEnter; // 플레이어 감지 이벤트
    private bool isPlayerInLobby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("로비 감지됨!");
            isPlayerInLobby = true; // 플레이어가 로비에 들어왔음을 기록
            onPlayerEnter?.Invoke(); // 이벤트 발생 (구독한 모든 함수 실행)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("로비에서 나감!");
            isPlayerInLobby = false; // 플레이어가 로비를 나갔음을 기록
        }
    }

    public bool IsPlayerInLobby()
    {
        return isPlayerInLobby;
    }
}
