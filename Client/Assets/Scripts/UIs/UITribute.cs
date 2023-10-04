using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TributeTypes
{
    Attack,
    Defence,
    Dodge,
    Speed,
    Health,
}
public class UITribute : UIBase
{
    private Dictionary<TributeTypes, TextMeshProUGUI> _statusTexts = new Dictionary<TributeTypes, TextMeshProUGUI>();
    private Dictionary<TributeTypes, int> _tributeLevels = new Dictionary<TributeTypes, int>();
    private Button _enterDungeonButton;
    //private Player _player;

    private static readonly int MAX_TRIBUTE_LEVEL = 10;

    private void Awake()
    {
        Initialize();
        LoadTributeLevel();
    }
    private void Start()
    {
        // 
        //GameManager.Instance.Player = _player;
        SetStatusTexts();
    }
    private void Initialize()
    {
        Transform tributeCategories = transform.Find("Tribute/Categories");
        foreach (TributeButton button in tributeCategories.GetComponentsInChildren<TributeButton>())
        {
            TributeTypes type = TributeTypes.Attack;
            string catrgoryName = button.transform.parent.name;

            if (catrgoryName == TributeTypes.Attack.ToString())
                type = TributeTypes.Attack;
            else if (catrgoryName == TributeTypes.Defence.ToString())
                type = TributeTypes.Defence;
            else if (catrgoryName == TributeTypes.Dodge.ToString())
                type = TributeTypes.Dodge;
            else if (catrgoryName == TributeTypes.Speed.ToString())
                type = TributeTypes.Speed;
            else if (catrgoryName == TributeTypes.Health.ToString())
                type = TributeTypes.Health;

            button.SetUp(type, this);
        }

        Transform statusCategories = transform.Find("PlayerStatus/Categories");
        for (int i = 0; i < Enum.GetValues(typeof(TributeTypes)).Length; i++)
        {
            TributeTypes type = (TributeTypes)i;
            _statusTexts.Add(type, statusCategories.transform.Find($"{type.ToString()}/Value").GetComponent<TextMeshProUGUI>());
            _tributeLevels.Add(type, 1);
        }

        _enterDungeonButton = transform.Find("EnterDungeonButton").GetComponent<Button>();
        _enterDungeonButton.onClick.AddListener(EnterDungeon);

    }

    private void EnterDungeon()
    {
        SaveTributeLevel();
        SceneManager.LoadScene("DungeonScene");
    }
    public void SetLevel(TributeTypes type, int level)
    {
        if (level < 1)
            level = 1;
        else if (level > MAX_TRIBUTE_LEVEL)
            level = MAX_TRIBUTE_LEVEL;

        _tributeLevels[type] = level;

        // 플레이어에게 스테이터스 반영.
        //_player.stat.Attack = level * level * 5;

        SetStatusTexts();
    }
    public int GetLevel(TributeTypes type)
    {
        return _tributeLevels[type];
    }
    private void SetStatusTexts()
    {
        // 플레이어 스탯 표시
        foreach (TributeTypes type in _statusTexts.Keys)
        {
            switch (type)
            {
                //case TributeTypes.Attack:
                //    _statusTexts[type].text = _player.stat.Attack;
                //    break;
                //case TributeTypes.Defence:
                //    _statusTexts[type].text = _player.stat.Defence;
                //    break;
                //case TributeTypes.Dodge:
                //    _statusTexts[type].text = _player.stat.Dodge;
                //    break;
                //case TributeTypes.Speed:
                //    _statusTexts[type].text = _player.stat.Speed;
                //    break;
                //case TributeTypes.Health:
                //    _statusTexts[type].text = _player.stat.Health;
                //    break;
            }
        }
    }
    private void SaveTributeLevel()
    {
        foreach (TributeTypes type in _tributeLevels.Keys)
        {
            PlayerPrefs.SetInt(type.ToString(), _tributeLevels[type]);
        }
        PlayerPrefs.Save();
    }
    private void LoadTributeLevel()
    {
        TributeTypes[] types = _tributeLevels.Keys.ToArray();
        foreach (TributeTypes type in types)
        {
            int level = PlayerPrefs.GetInt(type.ToString());
            SetLevel(type, level);
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
