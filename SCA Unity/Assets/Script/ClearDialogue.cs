using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ClearDialogue:MonoBehaviour
{
    public Text Name;       // Legacy Text - ĳ���� �̸�
    public Text context;    // Legacy Text - ���

    private Queue<DialogueLine> dialogueQueue; // ��� ť
    private bool isDialogueActive = false;     // ��ȭ ������ Ȯ��

    public Image fadeOutPanel;

    public AudioSource bgm;

    [System.Serializable]
    public class DialogueLine
    {
        public string characterName;
        public string dialogueText;
    }

    private void Start()
    {
        dialogueQueue = new Queue<DialogueLine>();

        // ��� �߰�
        dialogueQueue.Enqueue(new DialogueLine { characterName = "�����", dialogueText = "�ƴ� ������ ��� �ڰ����� �� ���ٰ�??" });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "�����", dialogueText = "�׷� �ڳ״� ���� �ְ��� ���ػ��̳�. �𱳸� �������." });
    
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (!isDialogueActive && dialogueQueue.Count > 0)
        {
            isDialogueActive = true;
            DisplayNextLine();
        }
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueQueue.Dequeue();
        Name.text = currentLine.characterName;
        context.text = currentLine.dialogueText;
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        Name.text = "";
        context.text = "";
        Debug.Log("��ȭ ����");

        StartCoroutine(FadeOutAndLoadScene()); // ���̵�ƿ� ����
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        float duration = 2f; // 2�� ���� ���̵�ƿ�
        float elapsedTime = 0f;
        Color color = fadeOutPanel.color;
        
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / duration); // ���İ� ���� (0 �� 1)
            fadeOutPanel.color = color;

            yield return null;
        }

        SceneManager.LoadScene("OpeningScene"); // �� ��ȯ
    }
}
