using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // �ִ� ü��
    [SerializeField] private TextMeshProUGUI healthText; // UI �ؽ�Ʈ ����
    [SerializeField] private Vector3 respawnPosition = new Vector3(0, 0, -7); // ������ ��ġ

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        healthText.text = "HP: " + currentHealth;
    }

    private void Die()
    {
        Debug.Log("Player Died");

        // ������ ó��
        Respawn();
    }

    private void Respawn()
    {
        // �÷��̾� ������
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.enabled = false; // CharacterController ��Ȱ��ȭ
        transform.position = respawnPosition; // ������ ��ġ�� �̵�
        characterController.enabled = true; // CharacterController Ȱ��ȭ

        // ü�� �ʱ�ȭ
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
}
