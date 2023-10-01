using Assets.Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Item> ItemDict { get; private set; } = new Dictionary<int, Item>();

    public void Init()
    {
        ItemDict = LoadJson<ItemData, int, Item>("ItemData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = ResourceManager.Instance.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
