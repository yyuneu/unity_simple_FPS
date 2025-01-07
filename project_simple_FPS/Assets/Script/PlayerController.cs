using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public GameObject eyes;
    public GameObject groundChecker;

    public Rigidbody rigidBody;

    Vector3 moveDirection;

    bool isGrounded;
    bool isOnSlope;

    [Header("Ground Check")]
    public float groundCheckingRadius = 0.5f; // 감지 범위
    public LayerMask whatIsGround;

    [Header("Basic Movement")]
    public float speedNormal = 5f; // 이동 속도
    public float groundDrag = 5f;
    public float airDrag = 1f;
    public float airSpeedRatio = 0.8f;

    [Header("Jump")]
    public float jumpForce = 5f; // 점프 힘

    [Header("Slope")]
    public float maxSlopeAngle = 45f; // 최대 경사각
    public float slopeSpeedRatio = 0.7f; // 경사면 이동 비율
    GameObject slopeGameObject;

    [Header("Respawn")]
    public float fallThreshold = -10f; // 플레이어가 떨어졌다고 간주할 y 좌표
    public Vector3 respawnPosition = new Vector3(0, 3, 0); // 리스폰 위치

    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        // 플레이어 회전
        transform.localRotation = InputManager.Instance.xQuat;

        // 입력 값 읽기
        float horizontalInput = InputManager.Instance.horizontalInput;
        float verticalInput = InputManager.Instance.VerticalInput;

        // 이동 방향 계산
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection.Normalize();

        // 지면 및 경사 상태 확인
        isGrounded = IsGrounded();
        isOnSlope = IsOnSlope();

        // 중력 처리
        if (isOnSlope)
            rigidBody.useGravity = false;
        else
            rigidBody.useGravity = true;

        // Drag 값 설정
        if (isGrounded)
            rigidBody.linearDamping = groundDrag;
        else
            rigidBody.linearDamping = airDrag;

        // 점프 처리
        if (Input.GetKeyDown(InputManager.Instance.jumpKey) && isGrounded)
        {
            Jump();
        }

        // 바닥으로 떨어졌는지 확인하고 리스폰
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }

        Debug.Log($"IsGrounded: {isGrounded}");
        Debug.Log($"Velocity: {rigidBody.linearVelocity}");
        Debug.Log($"Position: {transform.position}");
    }

    private void FixedUpdate()
    {
        MovePlayer();
        LimitVelocity(); // 속도 제한
    }

    void MovePlayer()
    {
        if (isOnSlope)
        {
            rigidBody.AddForce(GetVectorOnSlope(moveDirection) * speedNormal * slopeSpeedRatio, ForceMode.Force);
        }
        else if (isGrounded)
        {
            rigidBody.AddForce(moveDirection * speedNormal, ForceMode.Force);
        }
        else
        {
            rigidBody.AddForce(moveDirection * speedNormal * airSpeedRatio, ForceMode.Force);
        }
    }

    void LimitVelocity()
    {
        Vector3 velocity = rigidBody.linearVelocity;
        float maxSpeed = 10f; // 최대 속도 제한
        if (velocity.magnitude > maxSpeed)
        {
            rigidBody.linearVelocity = velocity.normalized * maxSpeed;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundChecker.transform.position, groundCheckingRadius);
    }

    bool IsGrounded()
    {
        bool result = Physics.CheckSphere(groundChecker.transform.position, groundCheckingRadius, whatIsGround);
        Debug.Log($"GroundChecker Position: {groundChecker.transform.position}, Radius: {groundCheckingRadius}, IsGrounded Result: {result}");
        return result;
    }

    void Jump()
    {
        rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, 0, rigidBody.linearVelocity.z); // 기존 Y 속도 초기화
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse); // 점프 힘 추가
    }

    bool IsOnSlope()
    {
        Collider[] col = Physics.OverlapSphere(groundChecker.transform.position, groundCheckingRadius, whatIsGround);
        if (col.Length != 0)
        {
            slopeGameObject = col[0].gameObject;

            float groundAngle = Vector3.Angle(Vector3.up, slopeGameObject.transform.up);

            if (5f < groundAngle && groundAngle < maxSlopeAngle)
                return true;
        }
        return false;
    }

    Vector3 GetVectorOnSlope(Vector3 vector)
    {
        Vector3 newVector;

        if (isOnSlope)
            newVector = Vector3.ProjectOnPlane(vector, slopeGameObject.transform.up).normalized;
        else
            newVector = vector;

        return newVector;
    }

    void Respawn()
    {
        rigidBody.linearVelocity = Vector3.zero; // 속도 초기화
        transform.position = respawnPosition; // 위치 리셋
    }
}
