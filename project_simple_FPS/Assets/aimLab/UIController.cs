using UnityEngine;
using TMPro;  // TextMeshProUGUI 사용하려면 추가

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  
    public TextMeshProUGUI timerText; 

    private void Start()
    {
       
    }

    // 점수 업데이트
    public void UpdateScore(int currentScore, int targetScore)
    {
        this.scoreText.text = currentScore.ToString() + "/" + targetScore.ToString(); 
    }

    // 남은 시간 업데이트
    public void UpdateTime(float timeLeft)
    {
        timerText.text = "Time Left: " + Mathf.Round(timeLeft).ToString() + "s";
    }
}
