using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam; // 플레이어 시점 카메라
    [SerializeField] private float damage = 10f; // 데미지 값
    [SerializeField] private LayerMask layerMask; // 타격 판정을 위한 레이어 마스크
    [SerializeField] private Crosshair crosshair; // Crosshair 스크립트 참조

    [Header("Fire Effects")]
    [SerializeField] private GameObject muzzleFlashEffect; // 총구 이펙트 (On/Off)

    [Header("Audio Clips")]
    [SerializeField] private AudioClip audioClipTakeOutWeapon; // 무기 장착 사운드
    [SerializeField] private AudioClip audioClipFireWeapon; // 총 발사 사운드

    private AudioSource audioSource; // 사운드 재생 컴포넌트
    private Animator animator; // 무기 애니메이션 컨트롤러

    private void Start()
    {
        animator = GetComponentInParent<Animator>(); // 부모 객체에서 Animator 가져오기
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

        // Fire 애니메이션 재생
        if (animator != null)
        {
            animator.Play("Fire", -1, 0); // Fire 애니메이션 재생
        }

        // 총구 이펙트 실행
        StartCoroutine(OnMuzzleFlashEffect());

        // 총 발사 사운드 재생
        PlaySound(audioClipFireWeapon);
    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        if (muzzleFlashEffect != null)
        {
            float duration = 0.1f; // 전체 이펙트 지속 시간
            float blinkInterval = 0.02f; // 깜빡이는 간격 (20ms)
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                muzzleFlashEffect.SetActive(true); // 이펙트 활성화
                yield return new WaitForSeconds(blinkInterval); // 20ms 대기
                muzzleFlashEffect.SetActive(false); // 이펙트 비활성화
                yield return new WaitForSeconds(blinkInterval); // 20ms 대기
                elapsedTime += blinkInterval * 2; // 활성화+비활성화 시간 누적
            }
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
        if (clip != null)
        {
            audioSource.Stop(); // 기존에 재생 중인 사운드를 정지
            audioSource.PlayOneShot(clip, 0.33f); // 볼륨을 1/3로 줄여 재생
        }
    }
}
