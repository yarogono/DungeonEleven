using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDungeonResult : UIBase
{
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private TextMeshProUGUI _roomText;
    [SerializeField]
    private TextMeshProUGUI _goldText;
    [SerializeField]
    private TextMeshProUGUI _killText;
    [SerializeField]
    private TextMeshProUGUI _bossKillText;
    [SerializeField]
    private TextMeshProUGUI _treasureBoxText;
    [SerializeField]
    private UISlots _slots;
    [SerializeField]
    private Button ExitButton;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        // 게임 매니저 생성용 ㅠㅠ  Player가 Static이라 매번 생성해줘야함...
        GameManager gameManager = GameManager.Instance;
        _healthSystem = GameManager.Player.GetComponent<HealthSystem>();
        ExitButton.onClick.AddListener(OnExitButtonClicked);
    }
    private void Start()
    {
        foreach (UISlot slot in _slots.GetAllSlots())
        {
            slot.IsLocked = false;
            slot.SetDraggable(false);
        }
        SetValue();

    }

    private void SetValue()
    {
        GameManager gameManager = GameManager.Instance;
        _timeText.text = gameManager.DungeonTime.ToString("F2");
        _roomText.text = (MapManager.Instance.GetMapIndex() + 1).ToString();
        PlayerInfo player = GameManager.PlayerInfo;
        _goldText.text = player.gold.ToString();
        // 적 킬수.
        //_killText.text = 

        // 보스 킬수
        //_bossKillText.text = 

        for (int i = 0; i < player.inventory.Count; i++)
        {
            _slots.GetSlotByIndex(i).Item = player.inventory[i];
        }
    }
    private void OnExitButtonClicked()
    {
        if (_healthSystem.CurrentHealth < 0)
        {
            // 플레이어 정보 및 레벨 초기화 진행해야 함.

            SceneManager.LoadScene("StartScene");
        }

        SceneManager.LoadScene("StoreScene");
    }

    public override void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public override void CloseUI()
    {
        gameObject.SetActive(false);
    }
    public void OnDestroy()
    {
        UIManager.Instance.RemoveUI(GetType().Name);
    }
}
