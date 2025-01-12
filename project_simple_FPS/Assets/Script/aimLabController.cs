using UnityEngine;
using UnityEngine.SceneManagement;

public class aimLabController : MonoBehaviour
{
    public int currentScore = 0;  // 현재 점수
    public int targetScore = 200;  // 목표 점수
    public int getScore = 10;  // 획득 점수
    public float gameTime = 30f;  // 게임 시간 (30초)
    public generator generator;
    private float timeLeft;  // 남은 시간
    public GameObject startTargetPrefab;

    private bool isGameOver = true;  // 초기 상태는 게임 종료 상태
    public bool isGameRunning = false;  // 게임 진행 여부
    public UIController uiController;
    Vector3 startPos = new Vector3(0, 2, -13); // 초기 타겟 위치

    void Start()
    {
        generator.StartGenerating(false);  // 게임 시작 전 타겟 생성 비활성화
        timeLeft = gameTime;  // 초기 남은 시간 설정

        // 초기 타겟 생성
        Instantiate(startTargetPrefab, startPos, Quaternion.identity);

        // 대화 컨트롤러에서 대화 시작
        DialogueController dialogueController = Object.FindFirstObjectByType<DialogueController>();
        dialogueController.StartDialogue();
    }

    void Update()
    {
        if (!isGameRunning) return;  // 게임이 진행 중이 아니면 업데이트 종료

        timeLeft -= Time.deltaTime;  // 남은 시간 감소

        // 시간이 0이 되면 게임 종료
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame(false);  // 실패 시 종료 처리
        }

        // UI에 남은 시간 업데이트
        uiController.UpdateTime(timeLeft);
    }

    public void IncreaseScore()
    {
        currentScore += getScore;
        uiController.UpdateScore(currentScore, targetScore);  // 점수 UI 업데이트
        Debug.Log(currentScore);

        // 목표 점수 도달 시 게임 종료 처리
        if (currentScore >= targetScore)
        {
            EndGame(true);  // 성공 시 종료 처리
        }
    }

    private void EndGame(bool isSuccess)
    {
        isGameOver = true;
        isGameRunning = false;
        generator.StartGenerating(false);  // 타겟 생성 비활성화

        if (isSuccess)
        {
            // 다음 씬 Adventure_demo로 전환
            Debug.Log("Game Completed! Loading Adventure_demo...");
            SceneManager.LoadScene("Adventure_demo"); // Adventure_demo 씬 로드
        }
        else
        {
            // 실패 시 다시 도전
            Debug.Log("Game Over! Try Again.");
            ResetGame();
        }
    }

    public void StartGame()
    {
        if (!isGameOver) return;  // 이미 게임이 진행 중이면 실행하지 않음

        isGameOver = false;
        isGameRunning = true;
        timeLeft = gameTime;  // 초기 시간 설정
        currentScore = 0;  // 점수 초기화
        uiController.UpdateScore(currentScore, targetScore);  // 점수 UI 초기화
        generator.StartGenerating(true);  // 타겟 생성 활성화
    }

    private void ResetGame()
    {
        isGameOver = true;  // 게임 종료 상태로 전환
        isGameRunning = false;  // 게임 진행 중지
        currentScore = 0;  // 점수 초기화
        timeLeft = gameTime;  // 남은 시간 초기화

        // 새로운 startTarget 생성
        Instantiate(startTargetPrefab, startPos, Quaternion.identity);

        uiController.UpdateScore(currentScore, targetScore);  // 점수 UI 초기화
        uiController.UpdateTime(timeLeft);  // 시간 UI 초기화
    }
}
