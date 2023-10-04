using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net;

public class GameManager : CustomSingleton<GameManager>
{
    [SerializeField] 
    public TMP_Text _dungeonTimeText;

    public static GameObject Player;
    public static PlayerInfo PlayerInfo;

    private float _dungeonTime;
    private bool _isTimerOn = false;
    public int Day;
    private void Start()
    {
        string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
        C_PlayerLogin playerLoginPacket = new C_PlayerLogin() { ip = externalip };
        NetworkManager.Instance.Send(playerLoginPacket.Write());
        Player = GameObject.FindWithTag("Player");
        if (Player != null)
        {
            PlayerStatsHandler playerStat = Player.GetComponent<PlayerStatsHandler>();
            PlayerInfo = playerStat.CurrentStates;
        }
    }

    private void Update()
    {
        if (_isTimerOn)
            UpdateDungeonTime();
    }

    private void UpdateDungeonTime()
    {
        _dungeonTime += Time.deltaTime;
        int minute = (int)_dungeonTime / 60;
        int second = (int)_dungeonTime % 60;

        _dungeonTimeText.text = $"{minute.ToString("00")}:{second.ToString("00")}";
    }

    public void StartDungeonTimer()
    {
        if (_dungeonTimeText == null)
            return;

        _isTimerOn = true;
    }


    public void StopDungeonTimer()
    {
        _isTimerOn = false;
        _dungeonTime = 0f;
    }

    public void PauseDungeonTimer()
    {
        _isTimerOn = false;
    }

    public string GetDungeonTime()
    {
        int minute = (int)_dungeonTime / 60;
        int second = (int)_dungeonTime % 60;
        return $"{minute.ToString("00")}:{second.ToString("00")}";
    }
}
