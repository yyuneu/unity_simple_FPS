using UnityEngine;

public class generator : MonoBehaviour
{
    public GameObject targetPrefab;
    public Vector3 minPosition = new Vector3(-6f, 2f, -14f);  // 생성될 위치의 최소값
    public Vector3 maxPosition = new Vector3(8f, 5f, -11f);  // 생성될 위치의 최대값
    public float spawnInterval = 1f;  // 생성 주기 (1초마다)
    bool isGenerating = false;        // 생성시작 여부

    float hitX;
    float hitY;
    float hitZ;
    bool isHit = false;
    public float hitRange = 2f;       //피격타겟의 근처값
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
        isGenerating = g;  // true=생성 시작 false=생성 종료

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
                    GameObject newHitTarget = Instantiate(targetPrefab, randomHitPosition, Quaternion.identity); // 랜덤하게 생성


                    Destroy(newHitTarget, spawnInterval);    // 1초후 자동 삭제(맞추지 못했음)
                    isHit = false;
                }

                else
                {
                    this.delta = 0;
                    float randomX = Random.Range(minPosition.x, maxPosition.x);
                    float randomY = Random.Range(minPosition.y, maxPosition.y);
                    float randomZ = Random.Range(minPosition.z, maxPosition.z);
                    Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
                    GameObject newTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity); // 랜덤하게 생성

                    Destroy(newTarget, spawnInterval);    // 1초후 자동 삭제(맞추지 못했음)
                }
            }

        }

    }

}
