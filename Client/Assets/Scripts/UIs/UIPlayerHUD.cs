using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHUD : MonoBehaviour
{
    private Slider _hpSlider;
    private TextMeshProUGUI _hpText;
    private TextMeshProUGUI _levelText;
    private Slider _timeSlider;
    private TextMeshProUGUI _timeText;
    private HealthSystem _healthSystem;
    private PlayerInfo _player;
    private GameManager _gameManager;
    private void Awake()
    {
        Initialize();

    }
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _player = GameManager.PlayerInfo;
        _healthSystem = GameManager.Player.GetComponent<HealthSystem>();
        _healthSystem.HealthChanged += HpUpdate;
        _gameManager.TimeChanged += TimeUpdate;
        SetStatus();
    }
    private void Initialize()
    {
        Transform Hpbar = transform.Find("Status/HPBar");
        _hpSlider = Hpbar.GetComponent<Slider>();
        _hpText = Hpbar.Find("Value/Text").GetComponent<TextMeshProUGUI>();
        _levelText = transform.Find("Status/Level/LevelText").GetComponent<TextMeshProUGUI>();
        Transform time = transform.Find("DayTime/Time");
        _timeSlider = time.GetComponent<Slider>();
        _timeText = time.Find("Value/Text").GetComponent<TextMeshProUGUI>();
    }
    private void SetStatus()
    {
        _levelText.text = _player.level.ToString();
        HpUpdate();
        TimeUpdate();

    }
    private void HpUpdate()
    {
        _hpSlider.value = _healthSystem.CurrentHealth / _healthSystem.MaxHealth;
        _hpText.text = (int)_healthSystem.CurrentHealth + " / " + (int)_healthSystem.MaxHealth;
    }
    private void TimeUpdate()
    {
        float remainTime = _gameManager.DungeonRemainTime;
        float limitTIme = _gameManager.DungeonExploreLimitTime;
        _timeSlider.value = remainTime / limitTIme;
        int minute = (int)(remainTime / 60);
        int second = (int)(remainTime % 60);
        _timeText.text = String.Format("{0:D2} / {1:D2}", minute, second);
    }

}
