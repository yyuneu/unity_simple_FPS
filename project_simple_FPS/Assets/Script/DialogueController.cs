using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string characterName; // ĳ���� �̸�
        [TextArea(2, 5)] public string[] sentences; // ��� �迭
    }

    public GameObject dialoguePanel; // ��ȭâ �г�
    public TextMeshProUGUI characterNameText; // ĳ���� �̸� �ؽ�Ʈ
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ
    public Dialogue[] dialogues; // ��ȭ �迭
    private int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    private int currentSentenceIndex = 0; // ���� ��� �ε���
    private bool isDialogueActive = false; // ��ȭ Ȱ��ȭ ����

    public PlayerMovement playerMovement; // �÷��̾� �̵� ��ũ��Ʈ ����
    public PlayerCamera playerCamera; // �÷��̾� ī�޶� ��ũ��Ʈ ����
    public PlayerGunController playerGunController; // �÷��̾� �ѱ� ��ũ��Ʈ ����

    private void Start()
    {
        dialoguePanel.SetActive(false); // ��ȭâ ��Ȱ��ȭ
    }

    public void StartDialogue()
    {
        if (dialogues.Length == 0)
        {
            Debug.LogWarning("No dialogues assigned.");
            return;
        }

        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        DisablePlayerControls();

        currentDialogueIndex = 0;
        currentSentenceIndex = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisplayNextSentence();
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    private void DisplayNextSentence()
    {
        if (currentDialogueIndex >= dialogues.Length)
        {
            EndDialogue();
            return;
        }

        Dialogue currentDialogue = dialogues[currentDialogueIndex];

        if (currentSentenceIndex < currentDialogue.sentences.Length)
        {
            characterNameText.text = currentDialogue.characterName;
            dialogueText.text = currentDialogue.sentences[currentSentenceIndex];
            currentSentenceIndex++;
        }
        else
        {
            currentDialogueIndex++;
            currentSentenceIndex = 0;

            if (currentDialogueIndex < dialogues.Length)
            {
                DisplayNextSentence();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        EnablePlayerControls();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisablePlayerControls()
    {
        if (playerMovement != null) playerMovement.enabled = false;
        if (playerCamera != null) playerCamera.enabled = false;
        if (playerGunController != null) playerGunController.enabled = false;
    }

    private void EnablePlayerControls()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (playerCamera != null) playerCamera.enabled = true;
        if (playerGunController != null) playerGunController.enabled = true;
    }

    public bool IsDialogueActive => isDialogueActive;
}
