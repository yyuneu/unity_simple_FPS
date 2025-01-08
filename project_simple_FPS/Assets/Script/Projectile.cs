using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f; // 투사체의 수명
    [SerializeField] private int damage = 10; // 투사체가 입히는 데미지

    private void Start()
    {
        // 일정 시간 후 파괴
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌 시 데미지를 입히고 투사체 삭제
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // 충돌 후 투사체 삭제
        Destroy(gameObject);
    }
}
