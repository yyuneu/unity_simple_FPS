using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 20f; // �ִ� ü��
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth; // ü�� �ʱ�ȭ
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

        // EnemyManager�� �� óġ ���� ����
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.EnemyKilled();
        }

        Destroy(gameObject); // �� ����
    }
}
