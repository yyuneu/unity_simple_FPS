using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 5f; // 기본 이동 속도
    [SerializeField] private float sprintMultiplier = 2f; // 스프린트 시 속도 배율
    [SerializeField] private float jumpForce = 5f; // 점프 힘
    [SerializeField] private float gravityScale = 1f; // 중력 가속도 스케일

    private CharacterController characterController;

    private Vector3 moveVec; // 캐릭터의 움직이는 방향
    private float moveH; // 좌측 이동방향
    private float moveV; // 전후 이동방향
    private float moveY; // 중력계산
    private float moveSpeed; // 현재 이동 속도

    // 특정 좌표로 리셋
    private Vector3 respawnPosition = new Vector3(0, 0, -7); // 리셋 위치
    private float fallThreshold = -10f; // 떨어지는 임계값

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        CheckFall();
    }

    private void Move()
    {
        moveH = Input.GetAxis("Horizontal"); // 수평 방향 입력 값
        moveV = Input.GetAxis("Vertical"); // 수직 방향 입력 값

        // 스프린트 처리
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? baseMoveSpeed * sprintMultiplier : baseMoveSpeed;

        if (characterController.isGrounded)
        {
            moveY = 0;

            // 점프 입력 처리
            if (Input.GetButtonDown("Jump"))
            {
                moveY = jumpForce;
            }
        }
        else
        {
            // 중력 가속도 적용
            moveY += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        // 로컬좌표를 기준으로 한 방식
        moveVec = transform.right * moveH + transform.forward * moveV;
        moveVec.y = moveY;

        // 이동
        characterController.Move(moveVec * Time.deltaTime * moveSpeed);
    }

    // 낙하 처리
    private void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            characterController.enabled = false; // 이동 중지
            transform.position = respawnPosition; // 위치 초기화
            characterController.enabled = true; // 이동 재개
        }
    }
}
