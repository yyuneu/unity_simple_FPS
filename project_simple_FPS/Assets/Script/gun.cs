using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam; // �÷��̾� ���� ī�޶�
    [SerializeField] private float damage = 10f; // ������ ��
    [SerializeField] private LayerMask layerMask; // Ÿ�� ������ ���� ���̾� ����ũ
    [SerializeField] private Crosshair crosshair; // Crosshair ��ũ��Ʈ ����

    [Header("Fire Effects")]
    [SerializeField] private GameObject muzzleFlashEffect; // �ѱ� ����Ʈ (On/Off)

    [Header("Audio Clips")]
    [SerializeField] private AudioClip audioClipTakeOutWeapon; // ���� ���� ����
    [SerializeField] private AudioClip audioClipFireWeapon; // �� �߻� ����
    [SerializeField] private AudioClip audioClipReload; // ������ ����

    [Header("Position Settings")]
    [SerializeField] private Vector3 positionOffset = new Vector3(0.5f, -0.3f, 0.7f); // ī�޶� ���� �� ��ġ ������
    [SerializeField] private float followSpeed = 10f; // ī�޶� ���󰡴� �ӵ� (����)

    [Header("Spawn Points")]
    [SerializeField] private Transform casingSpawnPoint; // ź�� ���� ��ġ

    [Header("Ammo Settings")]
    [SerializeField] private int magazineSize = 30; // źâ ũ��
    [SerializeField] private int totalAmmo = 120; // �Ѿ� �ѷ�
    private int currentAmmo; // ���� źâ �� �Ѿ� ��

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammoText; // ���� �Ѿ� UI

    private AudioSource audioSource; // ���� ��� ������Ʈ
    private Animator animator; // ���� �ִϸ��̼� ��Ʈ�ѷ�
    private CasingMemoryPool casingMemoryPool; // ź�� ���� �޸� Ǯ
    private bool isReloading = false; // ������ ������ Ȯ��

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>(); // �θ� ��ü���� Animator ��������
        casingMemoryPool = GetComponent<CasingMemoryPool>(); // CasingMemoryPool ������Ʈ ��������
        currentAmmo = magazineSize; // �ʱ� źâ �Ѿ� ����

        UpdateAmmoUI(); // �ʱ� �Ѿ� UI ������Ʈ

        // �ʱ�ȭ
        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(false); // �ѱ� ����Ʈ �ʱ�ȭ
        }
    }

    private void Update()
    {
        // ī�޶� �������� �� ��ġ �� ȸ�� ������Ʈ
        SmoothFollowCamera();

        // Reload ó��
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < magazineSize && totalAmmo > 0)
        {
            StartReload();
        }

        // �ڵ� ������
        if (currentAmmo <= 0 && !isReloading && totalAmmo > 0)
        {
            StartReload();
        }
    }

    private void SmoothFollowCamera()
    {
        if (cam == null) return;

        // ��ǥ ��ġ: ī�޶� ��ġ + ������
        Vector3 targetPosition = cam.transform.position + cam.transform.TransformDirection(positionOffset);

        // �ε巴�� ��ġ �̵�
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // ī�޶��� ȸ���� ����
        transform.rotation = Quaternion.Slerp(transform.rotation, cam.transform.rotation, followSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        // ������ �߿��� �߻� �Ұ�
        if (isReloading) return;

        // �Ѿ��� ���� ��� �߻� �Ұ�
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo! Reloading automatically...");
            return;
        }

        // Raycast�� Ÿ�� ����
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            IDamagable target = hit.transform.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeHit(damage);
                Debug.Log($"{hit.collider.name} was hit for {damage} damage.");
            }
        }

        // Crosshair ũ�� ���� Ʈ����
        if (crosshair != null)
        {
            crosshair.OnFire();
        }

        // Fire �ִϸ��̼� ���
        if (animator != null)
        {
            animator.Play("Fire", -1, 0); // Fire �ִϸ��̼� ���
        }

        // �ѱ� ����Ʈ ����
        StartCoroutine(OnMuzzleFlashEffect());

        // �� �߻� ���� ���
        PlaySound(audioClipFireWeapon);

        // ź�� ����
        SpawnCasing();

        // źâ���� �Ѿ� ����
        currentAmmo--;
        UpdateAmmoUI(); // �Ѿ� UI ������Ʈ
        Debug.Log($"Ammo: {currentAmmo}/{totalAmmo}");
    }

    private void StartReload()
    {
        // �̹� ������ ���̰ų� źâ�� ���� �� ������ ���ε� �Ұ�
        if (isReloading || currentAmmo == magazineSize || totalAmmo <= 0) return;

        StartCoroutine(OnReload());
    }

    private IEnumerator OnReload()
    {
        isReloading = true;

        // ������ �ִϸ��̼� �� ���� ���
        animator.SetTrigger("onReload");
        PlaySound(audioClipReload);

        // ������ ��� �ð� (�ִϸ��̼� ���̿� ���� ����)
        yield return new WaitForSeconds(2.0f);

        // źâ�� ���� �Ѿ� ä���
        int ammoNeeded = magazineSize - currentAmmo; // �ʿ��� �Ѿ� ��
        int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo); // ���� �Ѿ˿��� �ʿ��� ��ŭ�� ���ε�
        currentAmmo += ammoToReload;
        totalAmmo -= ammoToReload;

        UpdateAmmoUI(); // �Ѿ� UI ������Ʈ
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
        // �޸� Ǯ�� �̿��� ź�� ����
        casingMemoryPool.SpawnCasing(casingSpawnPoint.position, transform.right);
    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(true);
            yield return new WaitForSeconds(0.05f); // �ѱ� ����Ʈ ���� �ð�
            muzzleFlashEffect.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // ���� ���� ���� ���
        PlaySound(audioClipTakeOutWeapon);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.Stop(); // ������ ��� ���� ���带 ����
            audioSource.PlayOneShot(clip, 0.2f); // ���� ����
        }
    }
}
