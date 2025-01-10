using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 500f; // ���� ����

    [SerializeField]
    private Transform playerBody;

    private float xRotation = 0f; // ī�޶� ���� ȸ�� ����

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // �ʱ� xRotation ���� 0���� �����Ͽ� ������ �ٶ󺸵��� ���� -> ����
        xRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.localRotation = Quaternion.identity; // �÷��̾� ��ü ȸ�� �ʱ�ȭ
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        // ���콺 �Է°� �б�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ī�޶� ���� ȸ�� ó��
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ȸ�� ���� ����

        // ī�޶� ȸ��
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); // �¿� ȸ���� �÷��̾� ��ü�� ȸ��
    }
}
