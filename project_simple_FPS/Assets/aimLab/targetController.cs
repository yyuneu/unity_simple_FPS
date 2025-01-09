using Unity.VisualScripting;
using UnityEngine;

public class targetControoler : MonoBehaviour, IDamagable
{

    [SerializeField]
    private float hp = 20f;
    
    public aimLabController controller;
    public GameObject startTarget;

    private void Start()
    {
        controller = Object.FindAnyObjectByType<aimLabController>();
    }
    public void TakeHit(float damage)
    {

        hp -= damage;
        if (hp <= 0)
        {   
            if (gameObject == startTarget)
            {
                controller.StartGame();         //startTarget ��ݽ� ���ӽ���
            }
            else
            {
                
                controller.IncreaseScore();     //target ��ݽ� ��������
            }
           
            Destroy(gameObject);
        }
    }

}
