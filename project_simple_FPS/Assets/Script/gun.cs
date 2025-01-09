using System.Collections;
using UnityEngine;
using TMPro;

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
    [SerializeField] private AudioClip audioClipReload; // 재장전 사운드

    [Header("Position Settings")]
    [SerializeField] private Vector3 positionOffset = new Vector3(0.5f, -0.3f, 0.7f); // 카메라 기준 총 위치 오프셋
    [SerializeField] private float followSpeed = 10f; // 카메라 따라가는 속도 (보간)

    [Header("Spawn Points")]
    [SerializeField] private Transform casingSpawnPoint; // 탄피 생성 위치

    [Header("Ammo Settings")]
    [SerializeField] private int magazineSize = 30; // 탄창 크기
    [SerializeField] private int totalAmmo = 120; // 총알 총량
    private int currentAmmo; // 현재 탄창 내 총알 수

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammoText; // 남은 총알 UI

    private AudioSource audioSource; // 사운드 재생 컴포넌트
    private Animator animator; // 무기 애니메이션 컨트롤러
    private CasingMemoryPool casingMemoryPool; // 탄피 생성 메모리 풀
    private bool isReloading = false; // 재장전 중인지 확인

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>(); // 부모 객체에서 Animator 가져오기
        casingMemoryPool = GetComponent<CasingMemoryPool>(); // CasingMemoryPool 컴포넌트 가져오기
        currentAmmo = magazineSize; // 초기 탄창 총알 설정

        UpdateAmmoUI(); // 초기 총알 UI 업데이트

        // 초기화
        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(false); // 총구 이펙트 초기화
        }
    }

    private void Update()
    {
        // 카메라를 기준으로 총 위치 및 회전 업데이트
        SmoothFollowCamera();

        // Reload 처리
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < magazineSize && totalAmmo > 0)
        {
            StartReload();
        }

        // 자동 재장전
        if (currentAmmo <= 0 && !isReloading && totalAmmo > 0)
        {
            StartReload();
        }
    }

    private void SmoothFollowCamera()
    {
        if (cam == null) return;

        // 목표 위치: 카메라 위치 + 오프셋
        Vector3 targetPosition = cam.transform.position + cam.transform.TransformDirection(positionOffset);

        // 부드럽게 위치 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 카메라의 회전을 따라감
        transform.rotation = Quaternion.Slerp(transform.rotation, cam.transform.rotation, followSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        // 재장전 중에는 발사 불가
        if (isReloading) return;

        // 총알이 없을 경우 발사 불가
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo! Reloading automatically...");
            return;
        }

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

        // 탄피 생성
        SpawnCasing();

        // 탄창에서 총알 차감
        currentAmmo--;
        UpdateAmmoUI(); // 총알 UI 업데이트
        Debug.Log($"Ammo: {currentAmmo}/{totalAmmo}");
    }

    private void StartReload()
    {
        // 이미 재장전 중이거나 탄창이 가득 차 있으면 리로드 불가
        if (isReloading || currentAmmo == magazineSize || totalAmmo <= 0) return;

        StartCoroutine(OnReload());
    }

    private IEnumerator OnReload()
    {
        isReloading = true;

        // 재장전 애니메이션 및 사운드 재생
        animator.SetTrigger("onReload");
        PlaySound(audioClipReload);

        // 재장전 대기 시간 (애니메이션 길이에 맞춰 조정)
        yield return new WaitForSeconds(2.0f);

        // 탄창에 남은 총알 채우기
        int ammoNeeded = magazineSize - currentAmmo; // 필요한 총알 수
        int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo); // 보유 총알에서 필요한 만큼만 리로드
        currentAmmo += ammoToReload;
        totalAmmo -= ammoToReload;

        UpdateAmmoUI(); // 총알 UI 업데이트
        Debug.Log($"Reloaded: Ammo: {currentAmmo}/{totalAmmo}");

        isReloading = false;
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo}/{totalAmmo}";
        }
    }

    private void SpawnCasing()
    {
        // 메모리 풀을 이용해 탄피 생성
        casingMemoryPool.SpawnCasing(casingSpawnPoint.position, transform.right);
    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(true);
            yield return new WaitForSeconds(0.05f); // 총구 이펙트 지속 시간
            muzzleFlashEffect.SetActive(false);
        }
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
            audioSource.PlayOneShot(clip, 0.2f); // 볼륨 조절
        }
    }
}
