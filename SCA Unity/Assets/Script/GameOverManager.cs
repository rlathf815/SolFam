using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //public GameObject gameOverUI; // ���� ���� UI ������Ʈ
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
        SceneManager.LoadScene("GameOverScene"); // GameOverScene���� �̵�
    }

    public void RestartGame()
    {
        Debug.Log("���� �����!");

        // ī�޶� ȸ�� �� ���� ����
        FirstPersonLook.canlook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // �÷��̾� �̵� ����
        FirstPersonMovement.canRun = true;
        FirstPersonMovement.speed = 5f;
        Crouch.movementSpeed = 3f;

        // ������ ���� �ʱ�ȭ
        yeppy_player.catched = false;

        // **�÷��̾� ȸ���� �ʱ�ȭ**
        //player.transform.rotation = Quaternion.identity;
        SceneManager.LoadScene("VirusScene"); // GameScene���� �̵� (Retry ��ư)
    }

    public void ExitGame()
    {
        Application.Quit(); // ���� ���� (Exit ��ư)
    }

    public void StartGame()
    {
        SceneManager.LoadScene("VirusScene");
    }


}
