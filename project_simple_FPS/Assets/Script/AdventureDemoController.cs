using UnityEngine;

public class AdventureDemoController : MonoBehaviour
{
    public DialogueController dialogueController; // DialogueController 참조

    private bool gameStarted = false;

    private void Start()
    {
        if (dialogueController != null)
        {
            dialogueController.StartDialogue();
        }
        else
        {
            Debug.LogError("DialogueController is not assigned.");
        }
    }

    private void Update()
    {
        if (!gameStarted && dialogueController != null && !dialogueController.IsDialogueActive)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        Debug.Log("Game started!");
        // 게임 시작 로직 추가 가능
    }
}
