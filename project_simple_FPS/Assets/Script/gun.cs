using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam; // 플레이어 시점 카메라
    [SerializeField] private float damage = 10f; // 데미지 값
    [SerializeField] private LayerMask layerMask; // 타격 판정을 위한 레이어 마스크
    [SerializeField] private Crosshair crosshair; // Crosshair 스크립트 참조

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip audioClipTakeOutWeapon; // 무기 장착 사운드

    private AudioSource audioSource; // 사운드 재생 컴포넌트

    private void Start()
    {
        // 초기화 작업
    }

    public void Shoot()
    {
        // Raycast로 타격 판정
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            IDamagable target = hit.transform.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeHit(damage);
                Debug.Log($"{hit.collider.name} was hit for {damage} damage.");
            }
        }

        // Crosshair 크기 변경 트리거
        if (crosshair != null)
        {
            crosshair.OnFire();
        }

        // 총 발사 로직
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            IDamagable target = hit.transform.GetComponent<IDamagable>();
            target?.TakeHit(damage);
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // 무기 장착 사운드 재생
        PlaySound(audioClipTakeOutWeapon);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop(); // 기존에 재생중인 사운드를 정지하고
        audioSource.clip = clip; // 새로운 사운드를 clip으로 교체 후
        audioSource.Play(); // 사운드 재생
    }
}
