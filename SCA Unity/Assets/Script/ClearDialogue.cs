using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ClearDialogue:MonoBehaviour
{
    public Text Name;       // Legacy Text - 캐릭터 이름
    public Text context;    // Legacy Text - 대사

    private Queue<DialogueLine> dialogueQueue; // 대사 큐
    private bool isDialogueActive = false;     // 대화 중인지 확인

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

        // 대사 추가
        dialogueQueue.Enqueue(new DialogueLine { characterName = "원장님", dialogueText = "아니 정말로 모든 자격증을 다 땄다고??" });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "원장님", dialogueText = "그래 자네는 이제 최강의 취준생이네. 퇴교를 허락하지." });
    
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
        Debug.Log("대화 종료");

        StartCoroutine(FadeOutAndLoadScene()); // 페이드아웃 실행
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
        float duration = 2f; // 2초 동안 페이드아웃
        float elapsedTime = 0f;
        Color color = fadeOutPanel.color;
        
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / duration); // 알파값 증가 (0 → 1)
            fadeOutPanel.color = color;

            yield return null;
        }

        SceneManager.LoadScene("OpeningScene"); // 씬 전환
    }
}
