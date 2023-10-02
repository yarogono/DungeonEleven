using UnityEngine;
using UnityEngine.UI;

public class GameManager : CustomSingleton<GameManager>
{
    [SerializeField] 
    private Text _dungeonTimeText;

    private float _dungeonTime;
    private bool _isTimerOn = false;


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

        _dungeonTimeText.text = $"{minute.ToString("00")} : {second.ToString("00")}";
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
        return $"{minute.ToString("00")} : {second.ToString("00")}";
    }
}
