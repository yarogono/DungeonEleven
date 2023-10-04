using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartScene : MonoBehaviour
{
    private Button _gameStartButton;
    private Button _gameExitButton;

    private void Awake()
    {
        _gameStartButton = transform.Find("StartButton").GetComponent<Button>();
        _gameExitButton = transform.Find("ExitButton").GetComponent<Button>();
        _gameStartButton.onClick.AddListener(GoToDungeon);
        _gameExitButton.onClick.AddListener(QuitGame);
    }

    private void GoToDungeon()
    {
        SceneManager.LoadScene("DungeonScene");
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
