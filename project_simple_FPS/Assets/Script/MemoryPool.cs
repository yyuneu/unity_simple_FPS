using UnityEngine;
using System.Collections.Generic;

public class MemoryPool
{
    // �޸� Ǯ�� �����Ǵ� ������Ʈ ����
    private class PoolItem
    {
        public bool isActive; // GameObject�� Ȱ��/��Ȱ��ȭ ����
        public GameObject gameObject; // ���� ���� ������Ʈ
    }

    private int increaseCount = 5; // ������ �� �߰� ������ ������Ʈ ����
    private int maxCount; // ���� ����Ʈ�� ��ϵ� ������Ʈ �� ����
    private int activeCount; // ���� Ȱ�� ������ ������Ʈ ����

    private GameObject poolObject; // ���� ��� ������Ʈ�� ������
    private List<PoolItem> poolItemList; // �����ϴ� ��� ������Ʈ ����Ʈ

    public int MaxCount => maxCount; // �� ������Ʈ ���� ������Ƽ
    public int ActiveCount => activeCount; // Ȱ�� ������Ʈ ���� ������Ƽ

    // ������
    public MemoryPool(GameObject poolObject)
    {
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstantiateObjects();
    }

    /// <summary>
    /// increaseCount ������ ������Ʈ�� ����
    /// </summary>
    private void InstantiateObjects()
    {
        maxCount += increaseCount;

        for (int i = 0; i < increaseCount; i++)
        {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject);
            poolItem.gameObject.SetActive(false);

            poolItemList.Add(poolItem);
        }
    }

    /// <summary>
    /// ���� ���� ����(Ȱ��/��Ȱ��) ��� ������Ʈ ����
    /// </summary>
    public void DestroyObjects()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject.Destroy(poolItemList[i].gameObject);
        }

        poolItemList.Clear();
    }

    /// <summary>
    /// ���� ��Ȱ�� ������ ������Ʈ �� �ϳ��� Ȱ��ȭ
    /// </summary>
    public GameObject ActivatePoolItem()
    {
        if (poolItemList == null) return null;

        // ��� ������Ʈ�� Ȱ��ȭ�� ���, ���ο� ������Ʈ ����
        if (maxCount == activeCount)
        {
            InstantiateObjects();
        }

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.isActive == false)
            {
                activeCount++;
                poolItem.isActive = true;
                poolItem.gameObject.SetActive(true);

                return poolItem.gameObject;
            }
        }

        return null;
    }

    /// <summary>
    /// Ư�� ������Ʈ�� ��Ȱ��ȭ
    /// </summary>
    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (poolItemList == null || removeObject == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.gameObject == removeObject)
            {
                activeCount--;
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);

                return;
            }
        }
    }

    /// <summary>
    /// ���� Ȱ�� ������ ��� ������Ʈ�� ��Ȱ��ȭ
    /// </summary>
    public void DeactivateAllPoolItems()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.gameObject != null && poolItem.isActive == true)
            {
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);
            }
        }

        activeCount = 0;
    }
}
