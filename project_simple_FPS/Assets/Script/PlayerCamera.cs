using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 500f; // ���� ����

    [SerializeField]
    private Transform playerBody;

    [SerializeField]
    private float recoilAmount = 2f; // �ݵ� ũ��
    [SerializeField]
    private float recoilRecoverySpeed = 5f; // �ݵ� ���� �ӵ�

    private float xRotation = 0f; // ī�޶� ���� ȸ�� ����

    private float currentMouseX;
    private float currentMouseY;

    private bool isRecoilActive = false; // �ݵ� ������ Ȯ���ϴ� �÷���

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
        // ���콺 �Է°� �б�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // FPS�� ������ ����
        if (!isRecoilActive) // �ݵ� ���� �ƴ� ���� ���콺 �Է� ó��
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        }

        // ī�޶� ȸ��
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void ApplyRecoil()
    {
        if (isRecoilActive) return; // �̹� �ݵ� ���̶�� ����
        StartCoroutine(HandleRecoil());
    }

    private IEnumerator HandleRecoil()
    {
        isRecoilActive = true;

        float targetXRotation = xRotation - recoilAmount; // ī�޶� ���� �ö�
        float initialXRotation = xRotation;

        // ���� �ݵ� �̵�
        while (xRotation > targetXRotation)
        {
            xRotation = Mathf.Lerp(xRotation, targetXRotation, Time.deltaTime * recoilRecoverySpeed);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            yield return null;
        }

        // ���� ��ġ�� ����
        while (xRotation < initialXRotation)
        {
            xRotation = Mathf.Lerp(xRotation, initialXRotation, Time.deltaTime * recoilRecoverySpeed);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            yield return null;
        }

        xRotation = initialXRotation; // ���������� �ʱ� ��ġ�� ����
        isRecoilActive = false; // �ݵ� ����
    }
}
