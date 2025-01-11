using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI tapToStartText; // "Tap to Start" 텍스트
    public float blinkSpeed = 1.5f; // 깜빡이는 속도

    private bool isFadingOut = true;

    void Update()
    {
        if (tapToStartText != null)
        {
            Color color = tapToStartText.color;
            color.a += (isFadingOut ? -1 : 1) * Time.deltaTime * blinkSpeed;

            if (color.a <= 0f)
            {
                color.a = 0f;
                isFadingOut = false;
            }
            else if (color.a >= 1f)
            {
                color.a = 1f;
                isFadingOut = true;
            }

            tapToStartText.color = color;
        }
    }
}
