using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair; // 조준점 RectTransform
    [SerializeField] private float fireSize = 15f; // 발사 시 크기
    [SerializeField] private float normalSize = 10f; // 기본 크기
    [SerializeField] private float resizeSpeed = 10f; // 크기 변경 속도
    [SerializeField] private float sizeResetDelay = 0.1f; // 크기 복원 대기 시간

    private bool isFiring = false; // 발사 중 여부

    private void Update()
    {
        // 발사 중이면 크기를 fireSize로 변경
        if (isFiring)
        {
            crosshair.sizeDelta = Vector2.Lerp(crosshair.sizeDelta, new Vector2(fireSize, fireSize), Time.deltaTime * resizeSpeed);
        }
        else
        {
            // 발사 후에는 normalSize로 복원
            crosshair.sizeDelta = Vector2.Lerp(crosshair.sizeDelta, new Vector2(normalSize, normalSize), Time.deltaTime * resizeSpeed);
        }
    }

    public void OnFire()
    {
        // 발사 이벤트
        isFiring = true;
        Invoke(nameof(ResetCrosshair), sizeResetDelay);
    }

    private void ResetCrosshair()
    {
        // 발사 후 대기 시간 후 복원
        isFiring = false;
    }
}
