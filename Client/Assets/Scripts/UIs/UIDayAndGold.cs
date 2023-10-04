using TMPro;
using UnityEngine;

public class UIDayAndGold : UIBase
{
    private GameObject _day;
    private TextMeshProUGUI _dayText;
    private GameObject _goldInHand;
    private TextMeshProUGUI _goldText;

    private PlayerInfo _player;

    private const string DAY_UI_NAME = "Day";
    private const string GOLD_UI_NAME = "GoldInHand";

    private void Awake()
    {
        _day = transform.Find(DAY_UI_NAME).gameObject;
        _dayText = _day.GetComponentInChildren<TextMeshProUGUI>();
        _goldInHand = transform.Find(GOLD_UI_NAME).gameObject;
        _goldText = _goldInHand.GetComponentInChildren<TextMeshProUGUI>();

    }
    private void Start()
    {
        _player = GameManager.PlayerInfo;
        UpdateDay();
        UpdateGold();
    }

    public override void OpenUI()
    {
        _day.SetActive(true);
        _goldInHand.SetActive(true);
        UpdateDay();
        UpdateGold();
    }

    public override void CloseUI()
    {
        _day.SetActive(false);
        _goldInHand.SetActive(false);
    }
    public void UpdateDay()
    {
        _dayText.text = "Day " + GameManager.Instance.Day;
    }
    public void UpdateGold()
    {
        _goldText.text = _player.gold + " G";
    }

}
