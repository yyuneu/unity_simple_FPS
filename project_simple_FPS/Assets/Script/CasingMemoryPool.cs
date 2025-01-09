using UnityEngine;

public class CasingMemoryPool : MonoBehaviour
{
    [SerializeField] private GameObject casingPrefab; // ź�� ������Ʈ
    private MemoryPool memoryPool; // ź�� �޸� Ǯ

    private void Awake()
    {
        memoryPool = new MemoryPool(casingPrefab);
    }

    public void SpawnCasing(Vector3 position, Vector3 direction)
    {
        // �޸� Ǯ���� ������Ʈ Ȱ��ȭ
        GameObject item = memoryPool.ActivatePoolItem();

        // ź�� ��ġ �� ȸ�� ����
        item.transform.position = position;
        item.transform.rotation = Random.rotation;

        // ź�� �ʱ� ����
        item.GetComponent<Casing>().Setup(memoryPool, direction);
    }
}
