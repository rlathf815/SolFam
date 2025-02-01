using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //public GameObject gameOverUI; // 게임 오버 UI 오브젝트

    private void Start()
    {
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
        SceneManager.LoadScene("GameScene"); // GameScene으로 이동 (Retry 버튼)
    }

    public void ExitGame()
    {
        Application.Quit(); // 게임 종료 (Exit 버튼)
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }


}
