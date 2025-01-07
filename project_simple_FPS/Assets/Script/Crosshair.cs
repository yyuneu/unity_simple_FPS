using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair; // ������ RectTransform
    [SerializeField] private float fireSize = 15f; // �߻� �� ũ��
    [SerializeField] private float normalSize = 10f; // �⺻ ũ��
    [SerializeField] private float resizeSpeed = 10f; // ũ�� ���� �ӵ�
    [SerializeField] private float sizeResetDelay = 0.1f; // ũ�� ���� ��� �ð�

    private bool isFiring = false; // �߻� �� ����

    private void Update()
    {
        // �߻� ���̸� ũ�⸦ fireSize�� ����
        if (isFiring)
        {
            crosshair.sizeDelta = Vector2.Lerp(crosshair.sizeDelta, new Vector2(fireSize, fireSize), Time.deltaTime * resizeSpeed);
        }
        else
        {
            // �߻� �Ŀ��� normalSize�� ����
            crosshair.sizeDelta = Vector2.Lerp(crosshair.sizeDelta, new Vector2(normalSize, normalSize), Time.deltaTime * resizeSpeed);
        }
    }

    public void OnFire()
    {
        // �߻� �̺�Ʈ
        isFiring = true;
        Invoke(nameof(ResetCrosshair), sizeResetDelay);
    }

    private void ResetCrosshair()
    {
        // �߻� �� ��� �ð� �� ����
        isFiring = false;
    }
}
