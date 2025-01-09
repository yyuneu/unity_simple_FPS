using UnityEngine;
using UnityEngine.UI;

public class AimSettingsManager : MonoBehaviour
{
    public Toggle circularToggle;  // 원형 에임 Toggle
    public Toggle squareToggle;    // 사각형 에임 Toggle
    public Toggle crossToggle;     // 십자형 에임 Toggle

    void Start()
    {
        // 각 Toggle에 이벤트 리스너 추가
        circularToggle.onValueChanged.AddListener(OnAimShapeChanged);
        squareToggle.onValueChanged.AddListener(OnAimShapeChanged);
        crossToggle.onValueChanged.AddListener(OnAimShapeChanged);
    }

    // 에임 모양 변경 시 호출되는 메서드
    void OnAimShapeChanged(bool isOn)
    {
        // 선택된 에임 모양에 따라 처리
        if (circularToggle.isOn)
        {
            Debug.Log("원형 에임 선택");
            // 원형 에임 처리 코드
        }
        else if (squareToggle.isOn)
        {
            Debug.Log("사각형 에임 선택");
            // 사각형 에임 처리 코드
        }
        else if (crossToggle.isOn)
        {
            Debug.Log("십자형 에임 선택");
            // 십자형 에임 처리 코드
        }
    }
}
