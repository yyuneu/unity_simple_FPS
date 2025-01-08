using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        // "Player" ������Ʈ �������� �ڽ� ������Ʈ��
        // "arms_assault_rifle_01" ������Ʈ�� Animator ������Ʈ�� �ִ�
        animator = GetComponentInChildren<Animator>();
    }

    public float MoveSpeed
    {
        set => animator.SetFloat("movementSpeed", value);
        get => animator.GetFloat("movementSpeed");
    }
}
