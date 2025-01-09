using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam; // �÷��̾� ���� ī�޶�
    [SerializeField] private float damage = 10f; // ������ ��
    [SerializeField] private LayerMask layerMask; // Ÿ�� ������ ���� ���̾� ����ũ
    [SerializeField] private Crosshair crosshair; // Crosshair ��ũ��Ʈ ����

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip audioClipTakeOutWeapon; // ���� ���� ����

    private AudioSource audioSource; // ���� ��� ������Ʈ

    private void Start()
    {
        // �ʱ�ȭ �۾�
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

        // �� �߻� ����
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
        // ���� ���� ���� ���
        PlaySound(audioClipTakeOutWeapon);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop(); // ������ ������� ���带 �����ϰ�
        audioSource.clip = clip; // ���ο� ���带 clip���� ��ü ��
        audioSource.Play(); // ���� ���
    }
}
