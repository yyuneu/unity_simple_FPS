using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 500f; // 감도 조정

    [SerializeField]
    private Transform playerBody;

    private float xRotation = 0f;

    private float currentMouseX;
    private float currentMouseY;

    private float targetMouseX;
    private float targetMouseY;

    [SerializeField]
    private float smoothFactor = 0.05f; // 적당한 부드러움 조정 (0.01~0.1 추천)

    // Unity 메시지 | 참조 0개
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Unity 메시지 | 참조 0개
    private void Update()
    {
        Look();
    }

    private void Look()
    {
        // 마우스 입력값 읽기
        targetMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        targetMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // FPS용 반응성 유지
        currentMouseX = Mathf.Lerp(currentMouseX, targetMouseX, 1f / (1f + smoothFactor));
        currentMouseY = Mathf.Lerp(currentMouseY, targetMouseY, 1f / (1f + smoothFactor));

        // 플레이어 시점이 정수락 범위를 벗어나지 않도록 제한
        xRotation -= currentMouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라 회전
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * currentMouseX);
    }
}
