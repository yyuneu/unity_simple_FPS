using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // 최대 체력
    [SerializeField] private TextMeshProUGUI healthText; // HP를 표시할 UI 텍스트
    [SerializeField] private Image damageEffect; // 데미지 이펙트 UI 이미지
    [SerializeField] private float damageEffectDuration = 0.5f; // 데미지 이펙트 지속 시간
    [SerializeField] private Vector3 respawnPosition = new Vector3(0, 0, -7); // 리스폰 위치

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        // 데미지 이펙트를 초기화: 화면 전체를 덮고 투명하게 설정
        if (damageEffect != null)
        {
            damageEffect.rectTransform.anchorMin = Vector2.zero;
            damageEffect.rectTransform.anchorMax = Vector2.one;
            damageEffect.rectTransform.offsetMin = Vector2.zero;
            damageEffect.rectTransform.offsetMax = Vector2.zero;
            damageEffect.color = new Color(1f, 0f, 0f, 0f); // 완전 투명
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        Debug.Log("Player Health: " + currentHealth);

        // 데미지 이펙트를 보여줌
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
            healthText.text = "HP: " + currentHealth; // 현재 체력을 UI에 업데이트
        }
    }

    private IEnumerator ShowDamageEffect()
    {
        // 빨간색 이펙트 활성화
        damageEffect.color = new Color(1f, 0f, 0f, 0.5f); // 투명도 0.5 설정
        yield return new WaitForSeconds(damageEffectDuration);
        // 이펙트 비활성화
        damageEffect.color = new Color(1f, 0f, 0f, 0f); // 투명도 0 설정
    }

    private void Die()
    {
        Debug.Log("Player Died");

        // 리스폰 처리
        Respawn();
    }

    private void Respawn()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.enabled = false; // CharacterController 비활성화
        transform.position = respawnPosition; // 리스폰 위치로 이동
        characterController.enabled = true; // CharacterController 활성화

        // 체력을 초기화하고 UI를 업데이트
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
}
