using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam; // �÷��̾� ���� ī�޶�
    [SerializeField] private float damage = 10f;
    [SerializeField] private LayerMask layerMask;

    [Header("Recoil Settings - Gun")]
    [SerializeField] private Transform gunTransform; // �� ������Ʈ Transform
    [SerializeField] private Vector3 recoilAmount = new Vector3(-0.1f, 0.05f, 0); // �ѱ� �ݵ� ��ġ
    [SerializeField] private Vector3 recoilRotation = new Vector3(-5f, 2f, 0f); // �ѱ� �ݵ� ȸ��
    [SerializeField] private float recoilSpeed = 5f; // �ѱ� �ݵ� ���� �ӵ�

    private Vector3 originalGunPosition; // �ѱ��� ���� ��ġ
    private Quaternion originalGunRotation; // �ѱ��� ���� ȸ��

    private void Start()
    {
        // ���� ��ġ�� ȸ���� ����
        originalGunPosition = gunTransform.localPosition;
        originalGunRotation = gunTransform.localRotation;
    }

    public void Shoot()
    {
        // Raycast�� Ÿ�� ����
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            IDamagable target = hit.transform.GetComponent<IDamagable>();
            target?.TakeHit(damage);
        }

        // �ѱ� �ݵ� ����
        StartCoroutine(HandleGunRecoil());
    }

    private IEnumerator HandleGunRecoil()
    {
        // �ѱ� �ݵ� ��ġ�� ȸ�� ����
        gunTransform.localPosition += recoilAmount;
        gunTransform.localRotation *= Quaternion.Euler(recoilRotation);

        // �ѱ� ���� ��ġ�� ȸ������ ����
        while (Vector3.Distance(gunTransform.localPosition, originalGunPosition) > 0.01f ||
               Quaternion.Angle(gunTransform.localRotation, originalGunRotation) > 0.1f)
        {
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, originalGunPosition, Time.deltaTime * recoilSpeed);
            gunTransform.localRotation = Quaternion.Lerp(gunTransform.localRotation, originalGunRotation, Time.deltaTime * recoilSpeed);
            yield return null;
        }

        // ��ġ �� ȸ�� ����
        gunTransform.localPosition = originalGunPosition;
        gunTransform.localRotation = originalGunRotation;
    }
}
