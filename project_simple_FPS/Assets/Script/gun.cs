using System.Collections;
using UnityEngine;

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

    [Header("Spawn Points")]
    [SerializeField] private Transform casingSpawnPoint; // ź�� ���� ��ġ

    private AudioSource audioSource; // ���� ��� ������Ʈ
    private Animator animator; // ���� �ִϸ��̼� ��Ʈ�ѷ�
    private CasingMemoryPool casingMemoryPool; // ź�� ���� �޸� Ǯ

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>(); // �θ� ��ü���� Animator ��������
        casingMemoryPool = GetComponent<CasingMemoryPool>(); // CasingMemoryPool ������Ʈ ��������
    }

    public void Shoot()
    {
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
            float duration = 0.1f; // ��ü ����Ʈ ���� �ð�
            float blinkInterval = 0.02f; // �����̴� ���� (20ms)
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                muzzleFlashEffect.SetActive(true); // ����Ʈ Ȱ��ȭ
                yield return new WaitForSeconds(blinkInterval); // 20ms ���
                muzzleFlashEffect.SetActive(false); // ����Ʈ ��Ȱ��ȭ
                yield return new WaitForSeconds(blinkInterval); // 20ms ���
                elapsedTime += blinkInterval * 2; // Ȱ��ȭ+��Ȱ��ȭ �ð� ����
            }
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
            audioSource.PlayOneShot(clip, 0.33f); // ������ 1/3�� �ٿ� ���
        }
    }
}
