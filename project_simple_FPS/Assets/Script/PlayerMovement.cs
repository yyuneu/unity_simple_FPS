using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f; // 이동속도

    private CharacterController characterController;

    private Vector3 moveVec; // 캐릭터의 움직이는 방향
    private float moveH; // 좌측 이동방향
    private float moveV; // 전후 이동방향
    private float moveY; // 중력계산

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
        moveH = Input.GetAxis("Horizontal"); //수평 방향 입력 값(예: A, D 키 또는 화살표 키)을 받음
        moveV = Input.GetAxis("Vertical"); //수직 방향 입력 값(예: W, S 키 또는 화살표 키)을 받음

        if (characterController.isGrounded)
        {
            moveY = 0;
        }
        else
        {
            moveY += Physics.gravity.y * Time.deltaTime;
        }

        // 월드좌표를 기준으로 한 방식 : moveVec = new Vector3(moveH, 0, moveV);
        // 로컬좌표를 기준으로 한 방식
        moveVec = transform.right * moveH + transform.forward * moveV;
        moveVec.y = moveY;

        characterController.Move(moveVec * Time.deltaTime * moveSpeed);
    }

    //낙하처리
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
