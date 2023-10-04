using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISellingResult : UIBase
{
    private int _income;
    private TextMeshProUGUI _sellingResultGoldText;
    private Button _tributeButton;
    private Button _dungeonButton;
    private void Awake()
    {
        _sellingResultGoldText = transform.Find("ResultPanel/ResultText/GoldText").GetComponent<TextMeshProUGUI>();
        _tributeButton = transform.Find("ResultPanel/TributeButton").GetComponent<Button>();
        _dungeonButton = transform.Find("ResultPanel/DungeonButton").GetComponent <Button>();
        _tributeButton.onClick.AddListener(OpenTribute);
        _dungeonButton.onClick.AddListener(EnterDungeon);
    }
    private void Start()
    {
        _sellingResultGoldText.text = _income.ToString() + "G ¸¦ ¹ú¾ú´Ù.";
    }
    public void SetIncome(int income)
    {
        _income = income;
    }
    private void OpenTribute()
    {
        UIManager.Instance.OpenUI(typeof(UITribute).Name);
        UIManager.Instance.CloseUI(GetType().Name);
    }
    private void EnterDungeon()
    {
        SceneManager.LoadScene("DungeonScene");
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
