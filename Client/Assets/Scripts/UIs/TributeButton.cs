using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TributeButton : MonoBehaviour
{
    private Button _button;
    private TributeTypes _type;
    private UITribute _tribute;
    private TextMeshProUGUI _tributeCostText;
    private int _cost;
    private UIDayAndGold _dayAndGold;
    //private Player _player;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
        _tributeCostText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        _dayAndGold = UIManager.Instance.GetUI(typeof(UIDayAndGold).Name) as UIDayAndGold;
        UpdateTributeGold();
        //_player = GameManager.Instance.Player;
    }
    public void SetUp(TributeTypes type, UITribute tribute)
    {
        _type = type;
        _tribute = tribute;
    }

    private void OnClicked()
    {
        int currentLevel = _tribute.GetLevel(_type);
        _tribute.SetLevel(_type, currentLevel + 1);
        //_player.gold -= _cost;
        UpdateTributeGold();
        _dayAndGold.UpdateGold();
    }
    private void UpdateTributeGold()
    {
        int currentLevel = _tribute.GetLevel(_type);
        _cost = currentLevel * currentLevel * 100;
        _tributeCostText.text = _cost + " G";
    }
}
