using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        // "Player" 오브젝트 기준으로 자식 오브젝트의
        // "arms_assault_rifle_01" 오브젝트에 Animator 컴포넌트가 있다
        animator = GetComponentInChildren<Animator>();
    }

    public void OnReload()
    {
        animator.SetTrigger("onReload");
    }
    public float MoveSpeed
    {
        set => animator.SetFloat("movementSpeed", value);
        get => animator.GetFloat("movementSpeed");
    }
}
