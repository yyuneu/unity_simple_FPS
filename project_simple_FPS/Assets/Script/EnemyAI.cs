using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float patrolRadius = 10f; // 순찰 반경
    [SerializeField] private float detectionRange = 15f; // 플레이어 감지 범위
    [SerializeField] private float shootingRange = 10f; // 사격 범위
    [SerializeField] private float shootingInterval = 1f; // 사격 간격
    [SerializeField] private GameObject projectilePrefab; // 투사체 프리팹
    [SerializeField] private Transform firePoint; // 발사 위치
    [SerializeField] private float projectileSpeed = 10f; // 투사체 속도
    [SerializeField] private float inaccuracy = 1f; // 사격 정확도 (값이 클수록 부정확)

    private NavMeshAgent agent;
    private Transform player;
    private Vector3 patrolTarget;
    private float lastShotTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform; // 플레이어 태그로 찾기
        SetRandomPatrolTarget();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }
    }

    private void SetRandomPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            patrolTarget = hit.position;
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomPatrolTarget();
        }
        agent.SetDestination(patrolTarget);
    }

    private void ShootAtPlayer()
    {
        if (Time.time - lastShotTime < shootingInterval)
            return;

        lastShotTime = Time.time;

        // 투사체 생성 및 발사
        Vector3 shootDirection = (player.position - firePoint.position).normalized;

        // 부정확도 추가
        shootDirection.x += Random.Range(-inaccuracy, inaccuracy);
        shootDirection.y += Random.Range(-inaccuracy, inaccuracy);
        shootDirection.z += Random.Range(-inaccuracy, inaccuracy);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * projectileSpeed;
    }


}
