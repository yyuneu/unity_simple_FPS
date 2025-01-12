using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 5f; // 기본 이동 속도
    [SerializeField] private float sprintMultiplier = 2f; // 스프린트 시 속도 배율
    [SerializeField] private float jumpForce = 5f; // 점프 힘
    [SerializeField] private float gravityScale = 1f; // 중력 가속도 스케일

    [SerializeField] private AudioClip walkSound; // 걷기 사운드 클립
    [SerializeField] private AudioClip runSound; // 뛰기 사운드 클립

    private CharacterController characterController;
    private Animator animator; // 애니메이션 컨트롤러
    private AudioSource audioSource; // 사운드 재생기

    private Vector3 moveVec; // 캐릭터의 움직이는 방향
    private float moveH; // 좌측 이동방향
    private float moveV; // 전후 이동방향
    private float moveY; // 중력계산
    private float moveSpeed; // 현재 이동 속도

    private Vector3 respawnPosition = new Vector3(0, 0, -7); // 리셋 위치
    private float fallThreshold = -10f; // 떨어지는 임계값

    public bool IsSprinting { get; private set; } // 스프린트 상태를 외부에서 확인 가능

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // 자식 오브젝트에서 Animator 가져오기
        audioSource = GetComponent<AudioSource>();

        // AudioSource 기본 설정
        audioSource.loop = true; // 반복 재생 활성화
        audioSource.spatialBlend = 0; // 2D 사운드로 설정
    }

    private void Update()
    {
        Move();
        CheckFall();
        HandleSound();
    }

    private void Move()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        IsSprinting = Input.GetKey(KeyCode.LeftShift); // LeftShift 키로 스프린트 상태 확인
        moveSpeed = IsSprinting ? baseMoveSpeed * sprintMultiplier : baseMoveSpeed;

        if (characterController.isGrounded)
        {
            moveY = 0;
            if (Input.GetButtonDown("Jump"))
            {
                moveY = jumpForce;
            }
        }
        else
        {
            moveY += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        moveVec = transform.right * moveH + transform.forward * moveV;
        moveVec.y = moveY;

        characterController.Move(moveVec * Time.deltaTime * moveSpeed);

        UpdateAnimation(moveH, moveV, IsSprinting);
    }

    private void UpdateAnimation(float moveH, float moveV, bool isSprinting)
    {
        float movementSpeed = new Vector3(moveH, 0, moveV).magnitude;
        if (movementSpeed > 0)
        {
            movementSpeed = isSprinting ? 1f : 0.5f;
        }
        animator.SetFloat("movementSpeed", movementSpeed);
    }

    private void HandleSound()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            AudioClip currentClip = isSprinting ? runSound : walkSound;

            if (audioSource.clip != currentClip || !audioSource.isPlaying)
            {
                audioSource.clip = currentClip;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            characterController.enabled = false;
            transform.position = respawnPosition;
            characterController.enabled = true;
        }
    }
}