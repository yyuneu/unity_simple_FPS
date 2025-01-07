using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 5f; // �⺻ �̵� �ӵ�
    [SerializeField] private float sprintMultiplier = 2f; // ������Ʈ �� �ӵ� ����
    [SerializeField] private float jumpForce = 5f; // ���� ��
    [SerializeField] private float gravityScale = 1f; // �߷� ���ӵ� ������

    private CharacterController characterController;

    private Vector3 moveVec; // ĳ������ �����̴� ����
    private float moveH; // ���� �̵�����
    private float moveV; // ���� �̵�����
    private float moveY; // �߷°��
    private float moveSpeed; // ���� �̵� �ӵ�

    // Ư�� ��ǥ�� ����
    private Vector3 respawnPosition = new Vector3(0, 0, -7); // ���� ��ġ
    private float fallThreshold = -10f; // �������� �Ӱ谪

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
        moveH = Input.GetAxis("Horizontal"); // ���� ���� �Է� ��
        moveV = Input.GetAxis("Vertical"); // ���� ���� �Է� ��

        // ������Ʈ ó��
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? baseMoveSpeed * sprintMultiplier : baseMoveSpeed;

        if (characterController.isGrounded)
        {
            moveY = 0;

            // ���� �Է� ó��
            if (Input.GetButtonDown("Jump"))
            {
                moveY = jumpForce;
            }
        }
        else
        {
            // �߷� ���ӵ� ����
            moveY += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        // ������ǥ�� �������� �� ���
        moveVec = transform.right * moveH + transform.forward * moveV;
        moveVec.y = moveY;

        // �̵�
        characterController.Move(moveVec * Time.deltaTime * moveSpeed);
    }

    // ���� ó��
    private void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            characterController.enabled = false; // �̵� ����
            transform.position = respawnPosition; // ��ġ �ʱ�ȭ
            characterController.enabled = true; // �̵� �簳
        }
    }
}
