using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f; // ����ü�� ����
    [SerializeField] private int damage = 10; // ����ü�� ������ ������

    private void Start()
    {
        // ���� �ð� �� �ı�
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� �浹 �� �������� ������ ����ü ����
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // �浹 �� ����ü ����
        Destroy(gameObject);
    }
}
