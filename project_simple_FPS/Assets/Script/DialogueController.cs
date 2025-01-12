using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string characterName; // 캐릭터 이름
        [TextArea(2, 5)] public string[] sentences; // 대사 배열
    }

    public GameObject dialoguePanel; // 대화창 패널
    public TextMeshProUGUI characterNameText; // 캐릭터 이름 텍스트
    public TextMeshProUGUI dialogueText; // 대사 텍스트
    public Dialogue[] dialogues; // 대화 배열
    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private int currentSentenceIndex = 0; // 현재 대사 인덱스
    private bool isDialogueActive = false; // 대화 활성화 여부

    public PlayerMovement playerMovement; // 플레이어 이동 스크립트 참조
    public PlayerCamera playerCamera; // 플레이어 카메라 스크립트 참조
    public PlayerGunController playerGunController; // 플레이어 총기 스크립트 참조

    private void Start()
    {
        dialoguePanel.SetActive(false); // 대화창 비활성화
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
