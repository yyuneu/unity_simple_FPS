using UnityEngine;

public class generator : MonoBehaviour
{
    public GameObject targetPrefab;
    public Vector3 minPosition = new Vector3(-6f, 2f, -14f);  // ������ ��ġ�� �ּҰ�
    public Vector3 maxPosition = new Vector3(8f, 5f, -11f);  // ������ ��ġ�� �ִ밪
    public float spawnInterval = 1f;  // ���� �ֱ� (1�ʸ���)
    bool isGenerating = false;        // �������� ����

    float hitX;
    float hitY;
    float hitZ;
    bool isHit = false;
    public float hitRange = 2f;       //�ǰ�Ÿ���� ��ó��
    public targetControoler target;
    float delta = 0;
    void Start()
    {

    }
    public void setPos(float x, float y, float z)
    {
        this.hitX = x;
        this.hitY = y;
        this.hitZ = z;
        isHit = true;

    }
    public void StartGenerating(bool g)
    {
        isGenerating = g;  // true=���� ���� false=���� ����

    }

    // Update is called once per frame
    void Update()
    {


        if (isGenerating)
        {

            this.delta += Time.deltaTime;
            if (this.delta > spawnInterval)
            {
                if (isHit == true)
                {

                    this.delta = 0;
                    float randomHitX = Random.Range(-hitRange, hitRange);
                    float randomHitY = Random.Range(-hitRange, hitRange);
                    float randomHitZ = Random.Range(-hitRange, hitRange);
                    Vector3 randomHitPosition = new Vector3(hitX + randomHitZ, hitY + randomHitX, hitZ + randomHitY);
                    if (randomHitPosition.y < 1.7) randomHitPosition.y += 1.7f;
                    GameObject newHitTarget = Instantiate(targetPrefab, randomHitPosition, Quaternion.identity); // �����ϰ� ����


                    Destroy(newHitTarget, spawnInterval);    // 1���� �ڵ� ����(������ ������)
                    isHit = false;
                }

                else
                {
                    this.delta = 0;
                    float randomX = Random.Range(minPosition.x, maxPosition.x);
                    float randomY = Random.Range(minPosition.y, maxPosition.y);
                    float randomZ = Random.Range(minPosition.z, maxPosition.z);
                    Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
                    GameObject newTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity); // �����ϰ� ����

                    Destroy(newTarget, spawnInterval);    // 1���� �ڵ� ����(������ ������)
                }
            }

        }

    }

}
