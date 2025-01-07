using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam; // 플레이어 시점 카메라
    [SerializeField] private float damage = 10f;
    [SerializeField] private LayerMask layerMask;

    [Header("Recoil Settings - Gun")]
    [SerializeField] private Transform gunTransform; // 총 오브젝트 Transform
    [SerializeField] private Vector3 recoilAmount = new Vector3(-0.1f, 0.05f, 0); // 총기 반동 위치
    [SerializeField] private Vector3 recoilRotation = new Vector3(-5f, 2f, 0f); // 총기 반동 회전
    [SerializeField] private float recoilSpeed = 5f; // 총기 반동 복구 속도

    private Vector3 originalGunPosition; // 총기의 원래 위치
    private Quaternion originalGunRotation; // 총기의 원래 회전

    private void Start()
    {
        // 원래 위치와 회전값 저장
        originalGunPosition = gunTransform.localPosition;
        originalGunRotation = gunTransform.localRotation;
    }

    public void Shoot()
    {
        // Raycast로 타격 판정
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            IDamagable target = hit.transform.GetComponent<IDamagable>();
            target?.TakeHit(damage);
        }

        // 총기 반동 적용
        StartCoroutine(HandleGunRecoil());
    }

    private IEnumerator HandleGunRecoil()
    {
        // 총기 반동 위치와 회전 적용
        gunTransform.localPosition += recoilAmount;
        gunTransform.localRotation *= Quaternion.Euler(recoilRotation);

        // 총기 원래 위치와 회전으로 복구
        while (Vector3.Distance(gunTransform.localPosition, originalGunPosition) > 0.01f ||
               Quaternion.Angle(gunTransform.localRotation, originalGunRotation) > 0.1f)
        {
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, originalGunPosition, Time.deltaTime * recoilSpeed);
            gunTransform.localRotation = Quaternion.Lerp(gunTransform.localRotation, originalGunRotation, Time.deltaTime * recoilSpeed);
            yield return null;
        }

        // 위치 및 회전 고정
        gunTransform.localPosition = originalGunPosition;
        gunTransform.localRotation = originalGunRotation;
    }
}
