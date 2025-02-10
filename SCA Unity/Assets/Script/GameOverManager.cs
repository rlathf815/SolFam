using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //public GameObject gameOverUI; // 게임 오버 UI 오브젝트
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats.Instance.onHealthChangedCallback += CheckGameOver;
    }

    private void CheckGameOver()
    {
        if (PlayerStats.Instance.Health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene"); // GameOverScene으로 이동
    }

    public void RestartGame()
    {
        Debug.Log("게임 재시작!");

        // 카메라 회전 및 조작 복구
        FirstPersonLook.canlook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 플레이어 이동 복구
        FirstPersonMovement.canRun = true;
        FirstPersonMovement.speed = 5f;
        Crouch.movementSpeed = 3f;

        // 유나쌤 상태 초기화
        yeppy_player.catched = false;

        // **플레이어 회전값 초기화**
        //player.transform.rotation = Quaternion.identity;
        SceneManager.LoadScene("VirusScene"); // GameScene으로 이동 (Retry 버튼)
    }

    public void ExitGame()
    {
        Application.Quit(); // 게임 종료 (Exit 버튼)
    }

    public void StartGame()
    {
        SceneManager.LoadScene("VirusScene");
    }


}
