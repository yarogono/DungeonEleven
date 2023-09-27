using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _maps;
    [SerializeField] private GameObject _startMap;
    [SerializeField] private GameObject _mapTransitionImage;
    private GameObject _currentMap;
    private MapTransition _mapTransition;
    private int _mapIndex = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _startMap.SetActive(true);
        _player.transform.localPosition = Vector3.zero;
        _mapTransition = _mapTransitionImage.GetComponent<MapTransition>();
        _currentMap = _startMap;

    }

    public void LoadNextMap() //TODO : ·£´ýÀ¸·Î ¸Ê »ý¼º
    {
        _currentMap.SetActive(false);
        _mapTransition.LoadingMap();
        _maps[_mapIndex].SetActive(true);
        _mapTransition.LoadedMap();
        _currentMap = _maps[_mapIndex];
        _player.transform.localPosition = Vector3.zero;
        _mapIndex++;

    }
}
