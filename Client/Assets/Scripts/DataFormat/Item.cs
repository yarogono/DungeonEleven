using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public int id;
    public string name;
}

[Serializable]
public class ItemData : ILoader<int, Item>
{
    public List<Item> items = new List<Item>();

    public Dictionary<int, Item> MakeDict()
    {
        Dictionary<int, Item> dict = new Dictionary<int, Item>();
        foreach (Item item in items)
            dict.Add(item.id, item);

        return dict;
    }
}
