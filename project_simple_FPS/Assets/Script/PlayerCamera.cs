using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 500f; // 감도 조정

    [SerializeField]
    private Transform playerBody;

    private float xRotation = 0f; // 카메라 상하 회전 각도

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // 초기 xRotation 값을 0으로 설정하여 정면을 바라보도록 설정 -> 실패
        xRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.localRotation = Quaternion.identity; // 플레이어 몸체 회전 초기화
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

        // 카메라 상하 회전 처리
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 각도 제한

        // 카메라 회전
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); // 좌우 회전은 플레이어 몸체를 회전
    }
}
