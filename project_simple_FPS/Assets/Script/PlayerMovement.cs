using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f; // 이동 속도
    private float jumpForce = 7f; // 점프 힘

    private Rigidbody rb;
    private bool isGrounded; // 캐릭터가 땅에 있는지 여부

    public Transform groundChecker; // Ground Checker 오브젝트 참조
    public float groundCheckRadius = 0.2f; // Ground Checker의 감지 반경
    public LayerMask groundLayer; // 땅으로 설정할 레이어

    private Vector3 respawnPosition = new Vector3(0, 0, -7); // 리스폰 위치
    private float fallThreshold = -10f; // 낙하 기준점

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGround();
        Move();
        CheckFall();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Move()
    {
        float moveH = Input.GetAxis("Horizontal"); // 가로 방향 입력 값
        float moveV = Input.GetAxis("Vertical"); // 세로 방향 입력 값

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(moveH, 0, moveV);
        moveDirection = transform.TransformDirection(moveDirection); // 로컬 -> 월드 좌표 변환
        moveDirection *= moveSpeed;

        // Rigidbody의 XZ 이동 처리
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveDirection.x;
        velocity.z = moveDirection.z;
        rb.linearVelocity = velocity;
    }

    private void CheckGround()
    {
        // Ground Checker 위치에서 감지 반경 내에 Ground Layer가 있는지 확인
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer);

        // 디버그용: Ground Checker의 감지 범위 시각화
        Debug.DrawLine(groundChecker.position, groundChecker.position + Vector3.down * groundCheckRadius, isGrounded ? Color.green : Color.red);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 위쪽으로 힘을 가함
    }

    private void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            transform.position = respawnPosition; // 위치 초기화
            rb.linearVelocity = Vector3.zero; // 속도 초기화
        }
    }
}
