using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 500f; // ���� ����

    [SerializeField]
    private Transform playerBody;

    private float xRotation = 0f;

    private float currentMouseX;
    private float currentMouseY;

    private float targetMouseX;
    private float targetMouseY;

    [SerializeField]
    private float smoothFactor = 0.05f; // ������ �ε巯�� ���� (0.01~0.1 ��õ)

    // Unity �޽��� | ���� 0��
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Unity �޽��� | ���� 0��
    private void Update()
    {
        Look();
    }

    private void Look()
    {
        // ���콺 �Է°� �б�
        targetMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        targetMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // FPS�� ������ ����
        currentMouseX = Mathf.Lerp(currentMouseX, targetMouseX, 1f / (1f + smoothFactor));
        currentMouseY = Mathf.Lerp(currentMouseY, targetMouseY, 1f / (1f + smoothFactor));

        // �÷��̾� ������ ������ ������ ����� �ʵ��� ����
        xRotation -= currentMouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ī�޶� ȸ��
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * currentMouseX);
    }
}
