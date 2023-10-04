using TMPro;
using UnityEngine;

public class UIDayAndGold : UIBase
{
    private GameObject _day;
    private TextMeshProUGUI _dayText;
    private GameObject _goldInHand;
    private TextMeshProUGUI _goldText;
    
    // 플레이어의 골드 정보가 있는 타입
    // private Player _player;

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
        // 게임 매니저를 통해서 플레이어 경로 캐싱.
        // _player = GameManager.Instance.Player;
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
        // 게임 매니저에서 Day 정보를 받아와서 세팅
        // _dayText.text = "Day " + GameManager.Instance.Day;
    }
    public void UpdateGold()
    {
        // 플레이어 정보에서 Gold를 가져와서 세팅.
        // _goldText.text = _player.gold + " G";
    }

}
