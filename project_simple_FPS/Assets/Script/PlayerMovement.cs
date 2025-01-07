using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f; // �̵��ӵ�

    private CharacterController characterController;

    private Vector3 moveVec; // ĳ������ �����̴� ����
    private float moveH; // ���� �̵�����
    private float moveV; // ���� �̵�����
    private float moveY; // �߷°��

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
        moveH = Input.GetAxis("Horizontal"); //���� ���� �Է� ��(��: A, D Ű �Ǵ� ȭ��ǥ Ű)�� ����
        moveV = Input.GetAxis("Vertical"); //���� ���� �Է� ��(��: W, S Ű �Ǵ� ȭ��ǥ Ű)�� ����

        if (characterController.isGrounded)
        {
            moveY = 0;
        }
        else
        {
            moveY += Physics.gravity.y * Time.deltaTime;
        }

        // ������ǥ�� �������� �� ��� : moveVec = new Vector3(moveH, 0, moveV);
        // ������ǥ�� �������� �� ���
        moveVec = transform.right * moveH + transform.forward * moveV;
        moveVec.y = moveY;

        characterController.Move(moveVec * Time.deltaTime * moveSpeed);
    }

    //����ó��
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
