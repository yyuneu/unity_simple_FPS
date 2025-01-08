using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float patrolRadius = 20f; // 순찰 반경
    [SerializeField] private float detectionRange = 25f; // 플레이어 감지 범위
    [SerializeField] private float shootingRange = 20f; // 사격 범위
    [SerializeField] private float shootingInterval = 0.5f; // 사격 간격
    [SerializeField] private GameObject projectilePrefab; // 투사체 프리팹
    [SerializeField] private Transform firePoint; // 발사 위치
    [SerializeField] private float projectileSpeed = 60f; // 투사체 속도
    [SerializeField] private float inaccuracy = 0.1f; // 사격 정확도

    private NavMeshAgent agent;
    private Transform player;
    private Vector3 patrolTarget;
    private float lastShotTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform; // 플레이어 태그로 찾기
    }

    private void Start()
    {
        SetRandomPatrolTarget();
    }

    private void Update()
    {
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on a NavMesh!");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange && !IsObstacleBetween())
        {
            FacePlayer(); // 플레이어를 향해 회전
            ShootAtPlayer();
            agent.isStopped = true; // 멈춤
        }
        else if (distanceToPlayer <= detectionRange)
        {
            agent.isStopped = false; // 이동 재개
            agent.SetDestination(player.position); // 플레이어를 향해 이동
        }
        else
        {
            agent.isStopped = false; // 이동 재개
            Patrol();
        }
    }

    private bool IsObstacleBetween()
    {
        // 적과 플레이어 사이에 장애물이 있는지 확인
        Ray ray = new Ray(firePoint.position, (player.position - firePoint.position).normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, shootingRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return false; // 장애물이 없음
            }
            else
            {
                Debug.Log("Obstacle detected: " + hit.collider.name);
                return true; // 장애물이 있음
            }
        }
        return false; // 장애물이 없음
    }

    private void SetRandomPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
        }
    }

    private void Patrol()
    {
        if (agent.pathPending || agent.remainingDistance > 0.5f)
            return;

        SetRandomPatrolTarget();
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(patrolTarget);
        }
    }

    private void FacePlayer()
    {
        // 플레이어를 향해 회전
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // 수직 방향 회전 방지
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void ShootAtPlayer()
    {
        if (Time.time - lastShotTime < shootingInterval)
            return;

        lastShotTime = Time.time;

        Vector3 shootDirection = (player.position - firePoint.position).normalized;

        // 부정확도 추가
        shootDirection.x += Random.Range(-inaccuracy, inaccuracy) * 0.03f;
        shootDirection.y += Random.Range(-inaccuracy, inaccuracy) * 0.03f;
        shootDirection.z += Random.Range(-inaccuracy, inaccuracy) * 0.03f;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * projectileSpeed;
    }
}
