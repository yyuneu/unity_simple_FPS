using UnityEngine;
using TMPro;  // TextMeshProUGUI ����Ϸ��� �߰�

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  
    public TextMeshProUGUI timerText; 

    private void Start()
    {
       
    }

    // ���� ������Ʈ
    public void UpdateScore(int currentScore, int targetScore)
    {
        this.scoreText.text = currentScore.ToString() + "/" + targetScore.ToString(); 
    }

    // ���� �ð� ������Ʈ
    public void UpdateTime(float timeLeft)
    {
        timerText.text = "Time Left: " + Mathf.Round(timeLeft).ToString() + "s";
    }
}
