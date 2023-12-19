using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public enum sortTypes
{
    All,
    Weapon,
    Armour,
    Accessory,
    Misc,
    Price,
}

public class UIStore : UIBase
{
    private UISlots _inventorySlots;
    private Dictionary<SlotTypes, UISlots> _storeSlots = new Dictionary<SlotTypes, UISlots>();
    private Dictionary<SlotTypes, int> _storeSlotLevels = new Dictionary<SlotTypes, int>();
    private Button _upgradeStoreButton;
    private Button _sellingStartButton;
    private Button _displayItemButton;
    private Button _disposeAsJunkButton;
    private Transform _upgradePanel;
    private Dictionary<SlotTypes, TextMeshProUGUI> _UpgradeCategoryLevelTexts = new Dictionary<SlotTypes, TextMeshProUGUI>();
    private Button _closeUpgradePanelButton;
    private UIDayAndGold _dayAndGold;

    private PlayerInfo _player;

    private static readonly float DISPOSE_PRICE_RATE = 0.2f;
    private static readonly int MAX_STORE_LEVEL = 6;
    private static readonly int STORE_CATEGORIES_COUNT = 4;



    private void Awake()
    {
        Initailize();
        SetButtonEvents();
        LoadStoreLevel();
    }
    private void Start()
    {
        _player = GameManager.PlayerInfo;
        _dayAndGold = UIManager.Instance.GetUI(typeof(UIDayAndGold).Name) as UIDayAndGold;
        LoadPlayerInventory();
    }
    private void Initailize()
    {
        Transform inventory = transform.Find("Inventory");

        _inventorySlots = inventory.GetComponentInChildren<UISlots>();
        foreach (UISlot slot in _inventorySlots.GetAllSlots())
        {
            slot.IsLocked = false;
        }
        _displayItemButton = inventory.Find("DisplayItemButton").GetComponent<Button>();
        _disposeAsJunkButton = inventory.Find("DisposeAsJunkButton").GetComponent<Button>();

        Transform store = transform.Find("Store");
        foreach (UISlots slots in store.GetComponentsInChildren<UISlots>())
        {
            SlotTypes category = SlotTypes.All;
            string categoryName = slots.transform.parent.name;
            if (categoryName == SlotTypes.Weapon.ToString())
                category = SlotTypes.Weapon;
            else if (categoryName == SlotTypes.Armour.ToString())
                category = SlotTypes.Armour;
            else if (categoryName == SlotTypes.Accessory.ToString())
                category = SlotTypes.Accessory;
            else if (categoryName == SlotTypes.Misc.ToString())
                category = SlotTypes.Misc;

            _storeSlots.Add(category, slots);
            _storeSlotLevels.Add(category, 1);
        }

        foreach (SlotTypes slotType in _storeSlots.Keys)
        {
            foreach (UISlot slot in _storeSlots[slotType].GetAllSlots())
            {
                slot.SlotType = slotType;
            }
        }
        _upgradeStoreButton = store.Find("UpgradeButton").GetComponent<Button>();
        _sellingStartButton = store.Find("SellingStartButton").GetComponent<Button>();

        _upgradePanel = transform.Find("UpgradePanel");
        Transform categories = _upgradePanel.Find("Categories");
        for (int i = 0; i < STORE_CATEGORIES_COUNT; i++)
        {
            SlotTypes type = (SlotTypes)(i + 1);
            if (categories == null)
                Debug.Log("널이래");
            Transform category = categories.GetChild(i);
            _UpgradeCategoryLevelTexts.Add(type, category.Find("Level").GetComponent<TextMeshProUGUI>());
        }
        _closeUpgradePanelButton = _upgradePanel.Find("CloseUpgradePanelButton").GetComponent<Button>();

    }
    private void SetButtonEvents()
    {
        _upgradeStoreButton.onClick.AddListener(OpenStoreUpgradePanel);
        _sellingStartButton.onClick.AddListener(SellingStart);
        _displayItemButton.onClick.AddListener(DisplayChoosedItems);
        _disposeAsJunkButton.onClick.AddListener(DisposeItemsAsJunk);
        _closeUpgradePanelButton.onClick.AddListener(CloseStoreUpgradePanel);
    }
    public void SortInventory(sortTypes type)
    {
        UISlot[] slots = _inventorySlots.GetUsableSlots();

        switch (type)
        {
            case sortTypes.All:
                for (int i = 0; i < slots.Length - 1; i++)
                {
                    for (int j = i + 1; j < slots.Length; j++)
                    {
                        if (slots[j - 1].SlotType > slots[j].SlotType)
                        {
                            slots[j - 1].SwapSlot(slots[j]);
                        }
                    }
                }
                break;
            case sortTypes.Weapon:
                SortingBySlotType(SlotTypes.Weapon);
                break;
            case sortTypes.Armour:
                SortingBySlotType(SlotTypes.Armour);
                break;
            case sortTypes.Accessory:
                SortingBySlotType(SlotTypes.Accessory);
                break;
            case sortTypes.Misc:
                SortingBySlotType(SlotTypes.Misc);
                break;
            case sortTypes.Price:
                for (int i = 0; i < slots.Length - 1; i++)
                {
                    for (int j = i + 1; j < slots.Length; j++)
                    {
                        if (slots[j - 1].Item.price > slots[j].Item.price)
                        {
                            slots[j - 1].SwapSlot(slots[j]);
                        }
                    }
                }
                break;
        }
    }
    private void SortingBySlotType(SlotTypes slotTypes)
    {
        foreach (UISlot slot in _inventorySlots.FindSlots(slot => slot.SlotType != slotTypes))
        {
            slot.SetHideItemIcon(true);
        }
        UISlot[] Items = _inventorySlots.FindSlots(slot => slot.SlotType == slotTypes);
        for (int i = 0; i < Items.Length; i++)
        {
            _inventorySlots.GetSlotByIndex(i).SwapSlot(Items[i]);
        }
    }
    private void DisplayChoosedItems()
    {
        foreach(UISlot slot in _inventorySlots.GetChoosedSlots())
        {
            if (slot.IsEmpty())
                continue;


            ItemType itemType = slot.Item.itemType;
            SlotTypes category = itemType switch
            {
                ItemType.Weapon => SlotTypes.Weapon,
                ItemType.Armor => SlotTypes.Armour,
                ItemType.Accessory => SlotTypes.Accessory,
                ItemType.Misc => SlotTypes.Misc,
                _ => SlotTypes.All
            };
            UISlot emptySlot = _storeSlots[category].GetEmptySlot();
            if (emptySlot == null)
                continue;
            emptySlot.SwapSlot(slot);

        }
    }

    private void DisposeItemsAsJunk()
    {
        foreach(UISlot slot in _inventorySlots.GetChoosedSlots())
        {
            if (slot.IsEmpty())
                continue;

            int price = slot.Item.price;
            _player.gold += (int)(price * DISPOSE_PRICE_RATE);
            _player.inventory.Remove(slot.Item);
            slot.Item = null;
        }
        _dayAndGold.UpdateGold();
    }
    private void OpenStoreUpgradePanel()
    {
        _upgradePanel.gameObject.SetActive(true);
    }
    private void CloseStoreUpgradePanel()
    {
        _upgradePanel.gameObject.SetActive(false);
    }
    private void SellingStart()
    {
        int income = 0;
        foreach (UISlots slots in _storeSlots.Values)
        {
            foreach (UISlot slot in slots.GetAllSlots())
            {
                if (slot.IsEmpty())
                    continue;
                income += slot.Item.price;
                _player.inventory.Remove(slot.Item);
                slot.Item = null;
            }
        }
        _dayAndGold.UpdateGold();

        _player.gold += income;

        SaveStoreLevel();

        UISellingResult resultUI = (UISellingResult)UIManager.Instance.GetUI(typeof(UISellingResult).Name);
        resultUI.SetIncome(income);
        UIManager.Instance.OpenUI(typeof(UISellingResult).Name);
        UIManager.Instance.CloseUI(GetType().Name);
    }
    private void UnlockSlot(SlotTypes slotType, int count)
    {
        UISlots category = _storeSlots[slotType];
        if (category == null)
            return;
        UISlot[] slots = category.GetAllSlots();
        if (count > slots.Length)
        {
            count = slots.Length;
        }

        for (int i = 0; i < count; i++)
        {
            slots[i].IsLocked = false;
        }
    }
    public int GetLevel(SlotTypes slotType)
    {
        return _storeSlotLevels[slotType];
    }
    public void SetLevel(SlotTypes slotType, int level)
    {
        if (level < 1)
            level = 1;
        else if (level > MAX_STORE_LEVEL)
            level = MAX_STORE_LEVEL;

        _storeSlotLevels[slotType] = level;
        // 레벨 + 1만큼 슬롯 해금.
        UnlockSlot(slotType, level + 1);
        _UpgradeCategoryLevelTexts[slotType].text = level + " 레벨";
    }

    private void SaveStoreLevel()
    {
        foreach (SlotTypes slotType in _storeSlotLevels.Keys)
        {
            PlayerPrefs.SetInt(slotType.ToString(), _storeSlotLevels[slotType]);
        }
        PlayerPrefs.Save();
    }
    private void LoadStoreLevel()
    {
        SlotTypes[] types = _storeSlotLevels.Keys.ToArray();
        foreach (SlotTypes slotType in types)
        {
            int level = PlayerPrefs.GetInt(slotType.ToString());
            SetLevel(slotType, level);
        }
    }
    private void LoadPlayerInventory()
    {
        for (int i = 0; i < _player.inventory.Count; i++)
        {
            _inventorySlots.GetSlotByIndex(i).Item = _player.inventory[i];
        }
    }
    public override void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public override void OpenUI()
    {
        gameObject.SetActive(true);
    }
}
