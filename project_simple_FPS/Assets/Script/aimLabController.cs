using UnityEngine;
using UnityEngine.SceneManagement;

public class aimLabController : MonoBehaviour
{
    public int currentScore = 0;  // ���� ����
    public int targetScore = 200;  // ��ǥ ����
    public int getScore = 10;  // ȹ�� ����
    public float gameTime = 30f;  // ���� �ð� (30��)
    public generator generator;
    private float timeLeft;  // ���� �ð�
    public GameObject startTargetPrefab;

    private bool isGameOver = true;  // �ʱ� ���´� ���� ���� ����
    public bool isGameRunning = false;  // ���� ���� ����
    public UIController uiController;
    Vector3 startPos = new Vector3(0, 2, -13); // �ʱ� Ÿ�� ��ġ

    void Start()
    {
        generator.StartGenerating(false);  // ���� ���� �� Ÿ�� ���� ��Ȱ��ȭ
        timeLeft = gameTime;  // �ʱ� ���� �ð� ����

        // �ʱ� Ÿ�� ����
        Instantiate(startTargetPrefab, startPos, Quaternion.identity);

        // ��ȭ ��Ʈ�ѷ����� ��ȭ ����
        DialogueController dialogueController = Object.FindFirstObjectByType<DialogueController>();
        dialogueController.StartDialogue();
    }

    void Update()
    {
        if (!isGameRunning) return;  // ������ ���� ���� �ƴϸ� ������Ʈ ����

        timeLeft -= Time.deltaTime;  // ���� �ð� ����

        // �ð��� 0�� �Ǹ� ���� ����
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame(false);  // ���� �� ���� ó��
        }

        // UI�� ���� �ð� ������Ʈ
        uiController.UpdateTime(timeLeft);
    }

    public void IncreaseScore()
    {
        currentScore += getScore;
        uiController.UpdateScore(currentScore, targetScore);  // ���� UI ������Ʈ
        Debug.Log(currentScore);

        // ��ǥ ���� ���� �� ���� ���� ó��
        if (currentScore >= targetScore)
        {
            EndGame(true);  // ���� �� ���� ó��
        }
    }

    private void EndGame(bool isSuccess)
    {
        isGameOver = true;
        isGameRunning = false;
        generator.StartGenerating(false);  // Ÿ�� ���� ��Ȱ��ȭ

        if (isSuccess)
        {
            // ���� �� Adventure_demo�� ��ȯ
            Debug.Log("Game Completed! Loading Adventure_demo...");
            SceneManager.LoadScene("Adventure_demo"); // Adventure_demo �� �ε�
        }
        else
        {
            // ���� �� �ٽ� ����
            Debug.Log("Game Over! Try Again.");
            ResetGame();
        }
    }

    public void StartGame()
    {
        if (!isGameOver) return;  // �̹� ������ ���� ���̸� �������� ����

        isGameOver = false;
        isGameRunning = true;
        timeLeft = gameTime;  // �ʱ� �ð� ����
        currentScore = 0;  // ���� �ʱ�ȭ
        uiController.UpdateScore(currentScore, targetScore);  // ���� UI �ʱ�ȭ
        generator.StartGenerating(true);  // Ÿ�� ���� Ȱ��ȭ
    }

    private void ResetGame()
    {
        isGameOver = true;  // ���� ���� ���·� ��ȯ
        isGameRunning = false;  // ���� ���� ����
        currentScore = 0;  // ���� �ʱ�ȭ
        timeLeft = gameTime;  // ���� �ð� �ʱ�ȭ

        // ���ο� startTarget ����
        Instantiate(startTargetPrefab, startPos, Quaternion.identity);

        uiController.UpdateScore(currentScore, targetScore);  // ���� UI �ʱ�ȭ
        uiController.UpdateTime(timeLeft);  // �ð� UI �ʱ�ȭ
    }
}
