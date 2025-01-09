using UnityEngine;

public class CasingMemoryPool : MonoBehaviour
{
    [SerializeField] private GameObject casingPrefab; // 탄피 오브젝트
    private MemoryPool memoryPool; // 탄피 메모리 풀

    private void Awake()
    {
        memoryPool = new MemoryPool(casingPrefab);
    }

    public void SpawnCasing(Vector3 position, Vector3 direction)
    {
        // 메모리 풀에서 오브젝트 활성화
        GameObject item = memoryPool.ActivatePoolItem();

        // 탄피 위치 및 회전 설정
        item.transform.position = position;
        item.transform.rotation = Random.rotation;

        // 탄피 초기 설정
        item.GetComponent<Casing>().Setup(memoryPool, direction);
    }
}
