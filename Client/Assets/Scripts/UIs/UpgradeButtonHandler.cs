using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonHandler : MonoBehaviour
{
    private Button _upgradeButton;
    //private Player _player;
    private UIStore _store;
    private SlotTypes _slotType;
    private TextMeshProUGUI _upgradeGoldText;
    private int _cost;
    private UIDayAndGold _dayAndGold;

    private void Awake()
    {
        //_player = GameManager.Instance.Player;
        _upgradeButton = GetComponent<Button>();
        _upgradeButton.onClick.AddListener(OnClicked);
        _upgradeGoldText = GetComponentInChildren<TextMeshProUGUI>();
        string categoryName = transform.parent.name;
        if (categoryName == SlotTypes.Weapon.ToString())
            _slotType = SlotTypes.Weapon;
        else if (categoryName == SlotTypes.Armour.ToString())
            _slotType = SlotTypes.Armour;
        else if (categoryName == SlotTypes.Accessory.ToString())
            _slotType = SlotTypes.Accessory;
        else if (categoryName == SlotTypes.Misc.ToString())
            _slotType = SlotTypes.Misc;
    }
    private void Start()
    {
        _store = (UIStore)UIManager.Instance.GetUI(typeof(UIStore).Name);
        _dayAndGold = UIManager.Instance.GetUI(typeof(UIDayAndGold).Name) as UIDayAndGold;
        UpdateUpgradeGold();

    }

    private void OnClicked()
    {
        int currentLevel = _store.GetLevel(_slotType);
        _store.SetLevel(_slotType, currentLevel + 1);
        UpdateUpgradeGold();
        //_player.Gold -= _cost;
        _dayAndGold.UpdateGold();
    }
    private void UpdateUpgradeGold()
    {
        int currentLevel = _store.GetLevel(_slotType);
        _cost = currentLevel * currentLevel * 100;
        _upgradeGoldText.text = _cost + " G";
    }
}
