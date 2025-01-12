using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 5f; // �⺻ �̵� �ӵ�
    [SerializeField] private float sprintMultiplier = 2f; // ������Ʈ �� �ӵ� ����
    [SerializeField] private float jumpForce = 5f; // ���� ��
    [SerializeField] private float gravityScale = 1f; // �߷� ���ӵ� ������

    [SerializeField] private AudioClip walkSound; // �ȱ� ���� Ŭ��
    [SerializeField] private AudioClip runSound; // �ٱ� ���� Ŭ��

    private CharacterController characterController;
    private Animator animator; // �ִϸ��̼� ��Ʈ�ѷ�
    private AudioSource audioSource; // ���� �����

    private Vector3 moveVec; // ĳ������ �����̴� ����
    private float moveH; // ���� �̵�����
    private float moveV; // ���� �̵�����
    private float moveY; // �߷°��
    private float moveSpeed; // ���� �̵� �ӵ�

    private Vector3 respawnPosition = new Vector3(0, 0, -7); // ���� ��ġ
    private float fallThreshold = -10f; // �������� �Ӱ谪

    public bool IsSprinting { get; private set; } // ������Ʈ ���¸� �ܺο��� Ȯ�� ����

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // �ڽ� ������Ʈ���� Animator ��������
        audioSource = GetComponent<AudioSource>();

        // AudioSource �⺻ ����
        audioSource.loop = true; // �ݺ� ��� Ȱ��ȭ
        audioSource.spatialBlend = 0; // 2D ����� ����
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

        IsSprinting = Input.GetKey(KeyCode.LeftShift); // LeftShift Ű�� ������Ʈ ���� Ȯ��
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