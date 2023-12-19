using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlots : MonoBehaviour
{
    private List<UISlot> _slots = new List<UISlot>();
    private UISlot _slotPrefab;
    private void Awake()
    {
        _slots.AddRange(GetComponentsInChildren<UISlot>());
        _slotPrefab = ResourceManager.Instance.Load<UISlot>(UISlot.SlotPrefabPath);
    }

    public void AddSlot()
    {
        UISlot slot = Instantiate(_slotPrefab);
        slot.transform.SetParent(transform);
        slot.transform.SetAsLastSibling();
        _slots.Add(slot);
    }
    public void RemoveSlot(UISlot slot)
    {
        _slots.Remove(slot);
        Destroy(slot.gameObject);
    }

    public UISlot FindSlot(Func<UISlot, bool> selector)
    {
        foreach (UISlot slot in _slots)
        {
            if (selector(slot))
            {
                return slot;
            }
        }
        return null;
    }
    public UISlot[] FindSlots(Func<UISlot, bool> selector)
    {
        List<UISlot> slots = new List<UISlot>();
        foreach (UISlot slot in _slots)
        {
            if (selector(slot))
            {
                slots.Add(slot);
            }
        }
        return slots.ToArray();
    }
    public UISlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
    public UISlot[] GetUsableSlots()
    {
        return FindSlots(slot => slot.IsLocked == false);
    }
    public UISlot[] GetChoosedSlots()
    {
        return FindSlots(slot => slot.IsChoosed);
    }
    public UISlot GetEmptySlot()
    {
        return FindSlot(slot => slot.IsEmpty());
    }
    public UISlot[] GetEmptySlots()
    {
        return FindSlots(slot => slot.IsEmpty());
    }
    public UISlot GetSlotByIndex(int index)
    {
        if (index > _slots.Count)
            return null;
        return _slots[index];
    }
}
