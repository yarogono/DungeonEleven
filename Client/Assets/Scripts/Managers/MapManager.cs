using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _maps;
    [SerializeField] private GameObject _startMap;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _startMap.SetActive(true);
        _player.transform.localPosition = Vector3.zero;
    }

    public void LoadNextMap()
    {
        _startMap.SetActive(false);
        _maps[0].SetActive(true);
        _player.transform.localPosition = Vector3.zero;

    }
}
