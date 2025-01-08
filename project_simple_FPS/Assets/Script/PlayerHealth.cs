using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // 최대 체력
    [SerializeField] private TextMeshProUGUI healthText; // UI 텍스트 참조
    [SerializeField] private Vector3 respawnPosition = new Vector3(0, 0, -7); // 리스폰 위치

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

        // 리스폰 처리
        Respawn();
    }

    private void Respawn()
    {
        // 플레이어 리스폰
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.enabled = false; // CharacterController 비활성화
        transform.position = respawnPosition; // 리스폰 위치로 이동
        characterController.enabled = true; // CharacterController 활성화

        // 체력 초기화
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
}
