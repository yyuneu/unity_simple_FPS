using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 500f; // 감도 조정

    [SerializeField]
    private Transform playerBody;

    [SerializeField]
    private float recoilAmount = 2f; // 반동 크기
    [SerializeField]
    private float recoilRecoverySpeed = 5f; // 반동 복구 속도

    private float xRotation = 0f; // 카메라 상하 회전 각도

    private float currentMouseX;
    private float currentMouseY;

    private bool isRecoilActive = false; // 반동 중인지 확인하는 플래그

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        // 마우스 입력값 읽기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // FPS용 반응성 유지
        if (!isRecoilActive) // 반동 중이 아닐 때만 마우스 입력 처리
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        }

        // 카메라 회전
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void ApplyRecoil()
    {
        if (isRecoilActive) return; // 이미 반동 중이라면 무시
        StartCoroutine(HandleRecoil());
    }

    private IEnumerator HandleRecoil()
    {
        isRecoilActive = true;

        float targetXRotation = xRotation - recoilAmount; // 카메라가 위로 올라감
        float initialXRotation = xRotation;

        // 위로 반동 이동
        while (xRotation > targetXRotation)
        {
            xRotation = Mathf.Lerp(xRotation, targetXRotation, Time.deltaTime * recoilRecoverySpeed);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            yield return null;
        }

        // 원래 위치로 복구
        while (xRotation < initialXRotation)
        {
            xRotation = Mathf.Lerp(xRotation, initialXRotation, Time.deltaTime * recoilRecoverySpeed);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            yield return null;
        }

        xRotation = initialXRotation; // 최종적으로 초기 위치로 고정
        isRecoilActive = false; // 반동 종료
    }
}
