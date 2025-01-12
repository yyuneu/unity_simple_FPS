using UnityEngine;

public class AdventureDemoController : MonoBehaviour
{
    public DialogueController dialogueController; // DialogueController ����

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
        // ���� ���� ���� �߰� ����
    }
}
