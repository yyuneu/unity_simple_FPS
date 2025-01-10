using UnityEngine;
using UnityEngine.InputSystem.XR;
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
    private bool isGameRunning = false;  // 게임 진행 여부
    public UIController uiController;
    Vector3 startPos = new Vector3(0, 2, -13); //startTarget 위치
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generator.StartGenerating(false);  // 게임이 시작되기 전에는 공 생성 X
        timeLeft = gameTime;  // 초기 남은 시간 설정
        
        Instantiate(startTargetPrefab,startPos,Quaternion.identity);  // 초기 startTarget 생성
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameRunning) return;  // 게임이 시작되지 않았으면 시간 감소 로직 실행하지 않음

        timeLeft -= Time.deltaTime;  // 남은 시간 감소

        // 시간이 0이 되면 게임 종료
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame();
        }

        uiController.UpdateTime(timeLeft);  // UI에 남은 시간 업데이트
    }

    // 점수 증가 함수
    public void IncreaseScore()
    {
        currentScore += getScore;
        uiController.UpdateScore(currentScore, targetScore);  // 점수 업데이트
        Debug.Log(currentScore);

        // 목표 점수에 도달하면 게임 종료
        if (currentScore >= targetScore)
        {
            EndGame();
        }
    }

    // 게임 종료 함수
    private void EndGame()
    {
        isGameOver = true;
        isGameRunning = false;
        generator.StartGenerating(false);  // 공 생성 멈추기
        ResetGame();
    }

    // 게임 시작 함수
    public void StartGame()
    {
        if (!isGameOver) return;  // 이미 게임이 진행 중이면 실행하지 않음

        isGameOver = false;
        isGameRunning = true;
        timeLeft = gameTime;  // 초기 시간 설정
        currentScore = 0;  // 점수 초기화
        uiController.UpdateScore(currentScore, targetScore);  // 점수 UI 초기화
        generator.StartGenerating(true);  // 공 생성 시작
      
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
        SceneManager.LoadScene(2);
    }
}
