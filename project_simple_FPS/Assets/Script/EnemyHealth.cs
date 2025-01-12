using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 20f; // 최대 체력
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth; // 체력 초기화
    }

    public void TakeHit(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        // EnemyManager에 적 처치 정보 전달
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.EnemyKilled();
        }

        Destroy(gameObject); // 적 제거
    }
}
