using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //public GameObject gameOverUI; // ���� ���� UI ������Ʈ

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
        SceneManager.LoadScene("GameOverScene"); // GameOverScene���� �̵�
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // GameScene���� �̵� (Retry ��ư)
    }

    public void ExitGame()
    {
        Application.Quit(); // ���� ���� (Exit ��ư)
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }


}
