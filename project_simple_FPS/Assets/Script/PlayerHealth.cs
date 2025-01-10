using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // �ִ� ü��
    [SerializeField] private TextMeshProUGUI healthText; // HP�� ǥ���� UI �ؽ�Ʈ
    [SerializeField] private Image damageEffect; // ������ ����Ʈ UI �̹���
    [SerializeField] private float damageEffectDuration = 0.5f; // ������ ����Ʈ ���� �ð�
    [SerializeField] private Vector3 respawnPosition = new Vector3(0, 0, -7); // ������ ��ġ

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        // ������ ����Ʈ�� �ʱ�ȭ: ȭ�� ��ü�� ���� �����ϰ� ����
        if (damageEffect != null)
        {
            damageEffect.rectTransform.anchorMin = Vector2.zero;
            damageEffect.rectTransform.anchorMax = Vector2.one;
            damageEffect.rectTransform.offsetMin = Vector2.zero;
            damageEffect.rectTransform.offsetMax = Vector2.zero;
            damageEffect.color = new Color(1f, 0f, 0f, 0f); // ���� ����
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        Debug.Log("Player Health: " + currentHealth);

        // ������ ����Ʈ�� ������
        if (damageEffect != null)
        {
            StartCoroutine(ShowDamageEffect());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth; // ���� ü���� UI�� ������Ʈ
        }
    }

    private IEnumerator ShowDamageEffect()
    {
        // ������ ����Ʈ Ȱ��ȭ
        damageEffect.color = new Color(1f, 0f, 0f, 0.5f); // ���� 0.5 ����
        yield return new WaitForSeconds(damageEffectDuration);
        // ����Ʈ ��Ȱ��ȭ
        damageEffect.color = new Color(1f, 0f, 0f, 0f); // ���� 0 ����
    }

    private void Die()
    {
        Debug.Log("Player Died");

        // ������ ó��
        Respawn();
    }

    private void Respawn()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.enabled = false; // CharacterController ��Ȱ��ȭ
        transform.position = respawnPosition; // ������ ��ġ�� �̵�
        characterController.enabled = true; // CharacterController Ȱ��ȭ

        // ü���� �ʱ�ȭ�ϰ� UI�� ������Ʈ
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
}
