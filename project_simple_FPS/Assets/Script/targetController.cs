using Unity.VisualScripting;
using UnityEngine;

public class targetControoler : MonoBehaviour, IDamagable
{

    [SerializeField]
    private float hp = 10f;

    public aimLabController controller;
    public GameObject startTarget;
    public generator generator;

    private void Start()
    {
        controller = Object.FindAnyObjectByType<aimLabController>();
        generator = Object.FindAnyObjectByType<generator>();
    }
    public void TakeHit(float damage)
    {

        hp -= damage;
        if (hp <= 0)
        {
            if (gameObject == startTarget)
            {
                controller.StartGame();         //startTarget 사격시 게임시작
            }
            else
            {

                controller.IncreaseScore();     //target 사격시 점수증가
                generator.setPos(transform.position.x, transform.position.y, transform.position.z);

            }

            Destroy(gameObject);
        }
    }

}
