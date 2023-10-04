using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingButtonHandler : MonoBehaviour
{
    private Button _sortButton;
    private UIStore _store;
    private sortTypes _sortType;

    private void Awake()
    {
        _sortButton = GetComponent<Button>();
        _sortButton.onClick.AddListener(OnClicked);

        string categoryName = transform.parent.name;
        if (categoryName == sortTypes.All.ToString())
            _sortType = sortTypes.All;
        else if (categoryName == sortTypes.Weapon.ToString())
            _sortType = sortTypes.Weapon;
        else if (categoryName == sortTypes.Armour.ToString())
            _sortType = sortTypes.Armour;
        else if (categoryName == sortTypes.Accessory.ToString())
            _sortType = sortTypes.Accessory;
        else if (categoryName == sortTypes.Misc.ToString())
            _sortType = sortTypes.Misc;
        else if (categoryName == sortTypes.Price.ToString())
            _sortType = sortTypes.Price;
    }
    private void Start()
    {
        _store = (UIStore)UIManager.Instance.GetUI(typeof(UIStore).Name);
    }

    private void OnClicked()
    {
        _store.SortInventory(_sortType);
    }
}
