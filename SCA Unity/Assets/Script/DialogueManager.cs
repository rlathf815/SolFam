using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text Name;       // Legacy Text - 캐릭터 이름
    public Text context;    // Legacy Text - 대사

    private Queue<DialogueLine> dialogueQueue; // 대사 큐
    private bool isDialogueActive = false;     // 대화 중인지 확인

    public Image fadeOutPanel;

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
        dialogueQueue.Enqueue(new DialogueLine { characterName = "나", dialogueText = "안녕하세요, 취업 준비를 하면서 컴퓨터 자격증을 좀 따고 싶어서 왔는데요." });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "원장님", dialogueText = "좋은 선택입니다. 저희 학원에서는 COSpro, ITQ, 컴활, 정처기, GTQ 등 다양한 자격증을 대비할 수 있습니다." });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "나", dialogueText = "취업하려면 어떤 자격증을 따야 할까요?" });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "원장님", dialogueText = "취업 시장에서 경쟁력을 갖추려면 당연히 다다익선이죠." });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "원장님", dialogueText = "그러니 자격증을 모두 취득하기 전까지는 학원을 떠날 수 없습니다. (미소)" });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "나", dialogueText = "…네? 방금 뭐라고 하셨죠?" });
        dialogueQueue.Enqueue(new DialogueLine { characterName = "원장님", dialogueText = "하하, 농담입니다. 부담 갖지 말고 열심히 해봅시다." });

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

        SceneManager.LoadScene("GameScene"); // 씬 전환
    }
}
