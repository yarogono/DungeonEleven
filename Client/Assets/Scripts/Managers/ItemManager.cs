using Assets.Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ItemManager : CustomSingleton<ItemManager> 
{
    private Queue<Item> _itemQueue = new Queue<Item>();

    private const int MIN_ITEM_COUNT = 0;
    private const int MAX_ITEM_COUNT = (int)ItemType.MaxCount;

    public void CreateItem(Vector3 monsterPos)
    {
        int randNum = Random.Range(MIN_ITEM_COUNT, MAX_ITEM_COUNT);
        Item createdItem = DataManager.Instance.ItemDict[randNum];

        if (createdItem == null)
            return;

        string createdItemPrefab = createdItem.prefab;
        GameObject itemGameObject = ResourceManager.Instance.Instantiate($"Items/{createdItemPrefab}");

        if (itemGameObject == null)
            return;

        itemGameObject.transform.position = monsterPos;
    }

    public void LootItem(GameObject itemGameObject)
    {
        Dictionary<int, Item> itemDict = DataManager.Instance.ItemDict;
        string lootItemName = itemGameObject.name;

        Item lootItem = null;
        foreach(Item item in itemDict.Values)
        {
            if (lootItemName.Equals(item.prefab))
                lootItem = item;
        }

        if (lootItem == null)
            return;

        SoundManager.Instance.Play("coin", Sound.Effect);
        _itemQueue.Enqueue(lootItem);
        
        DestroyItem(itemGameObject);
    }

    public void DestroyItem(GameObject itemGameObject) 
    {
        if (itemGameObject == null)
            return;

        ResourceManager.Instance.Destroy(itemGameObject);
    }
}
