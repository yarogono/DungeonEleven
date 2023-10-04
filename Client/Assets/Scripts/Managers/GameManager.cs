using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net;
using System;
using UnityEngine.SceneManagement;

public class GameManager : CustomSingleton<GameManager>
{
    [SerializeField] 

    public static GameObject Player;
    public static PlayerInfo PlayerInfo;

    private float _dungeonTime;
    public float DungeonTime { get { return _dungeonTime; } }
    public float DungeonRemainTime { get; private set; }
    public float DungeonExploreLimitTime { get; private set; }
    public event Action TimeChanged;
    private bool _isTimerOn = false;
    public int Day;
    private void Awake()
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
        if (SceneManager.GetActiveScene().name == "DungeonScene")
            StartDungeonTimer();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void Update()
    {
        if (_isTimerOn)
            UpdateDungeonTime();
    }

    private void UpdateDungeonTime()
    {
        _dungeonTime += Time.deltaTime;
        DungeonRemainTime = DungeonExploreLimitTime - _dungeonTime;
        TimeChanged?.Invoke();
    }

    public void StartDungeonTimer()
    {
        DungeonExploreLimitTime = Mathf.Sqrt(PlayerInfo.speed) * 2 * 60;
        _isTimerOn = true;
    }
    private void OnActiveSceneChanged(Scene previousScene, Scene newScene)
    {
        if (newScene.name == "DungeonScene")
        {
            StartDungeonTimer();
        }
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
