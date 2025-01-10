using UnityEngine;
using UnityEngine.InputSystem.XR;
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
    private bool isGameRunning = false;  // ���� ���� ����
    public UIController uiController;
    Vector3 startPos = new Vector3(0, 2, -13); //startTarget ��ġ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generator.StartGenerating(false);  // ������ ���۵Ǳ� ������ �� ���� X
        timeLeft = gameTime;  // �ʱ� ���� �ð� ����
        
        Instantiate(startTargetPrefab,startPos,Quaternion.identity);  // �ʱ� startTarget ����
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameRunning) return;  // ������ ���۵��� �ʾ����� �ð� ���� ���� �������� ����

        timeLeft -= Time.deltaTime;  // ���� �ð� ����

        // �ð��� 0�� �Ǹ� ���� ����
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame();
        }

        uiController.UpdateTime(timeLeft);  // UI�� ���� �ð� ������Ʈ
    }

    // ���� ���� �Լ�
    public void IncreaseScore()
    {
        currentScore += getScore;
        uiController.UpdateScore(currentScore, targetScore);  // ���� ������Ʈ
        Debug.Log(currentScore);

        // ��ǥ ������ �����ϸ� ���� ����
        if (currentScore >= targetScore)
        {
            EndGame();
        }
    }

    // ���� ���� �Լ�
    private void EndGame()
    {
        isGameOver = true;
        isGameRunning = false;
        generator.StartGenerating(false);  // �� ���� ���߱�
        ResetGame();
    }

    // ���� ���� �Լ�
    public void StartGame()
    {
        if (!isGameOver) return;  // �̹� ������ ���� ���̸� �������� ����

        isGameOver = false;
        isGameRunning = true;
        timeLeft = gameTime;  // �ʱ� �ð� ����
        currentScore = 0;  // ���� �ʱ�ȭ
        uiController.UpdateScore(currentScore, targetScore);  // ���� UI �ʱ�ȭ
        generator.StartGenerating(true);  // �� ���� ����
      
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
        SceneManager.LoadScene(2);
    }
}
