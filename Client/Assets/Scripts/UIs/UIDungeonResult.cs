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
        // ���� �Ŵ��� ������ �Ф�  Player�� Static�̶� �Ź� �����������...
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
        // �� ų��.
        //_killText.text = 

        // ���� ų��
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
            // �÷��̾� ���� �� ���� �ʱ�ȭ �����ؾ� ��.

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
