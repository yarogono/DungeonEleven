using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _maps;
    [SerializeField] private GameObject[] _sideMaps;
    [SerializeField] private GameObject _startMap;
    [SerializeField] private GameObject _bossMap;
    [SerializeField] private GameObject _mapTransitionImage;
    private TestPlayerMovement _playerController;
    private GameObject _currentMap = null;
    private MapTransition _mapTransition;
    private int _mapIndex = 0;
    public bool isFinalMain = false;
    private Vector3 _previousPosition = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _startMap.SetActive(true);
        _player.transform.localPosition = Vector3.zero;
        _playerController = _player.GetComponent<TestPlayerMovement>();
        _mapTransition = _mapTransitionImage.GetComponent<MapTransition>();
        _currentMap = _startMap;
        _maps = _maps.OrderBy(x => UnityEngine.Random.Range(-6, 6)).ToArray();
        _sideMaps = _sideMaps.OrderBy(x => UnityEngine.Random.Range(-6, 6)).ToArray();

    }

    public void LoadNextMap(PortalType type) 
    {
        try
        {
            switch (type)
            {
                case PortalType.MAIN:
                    _currentMap?.SetActive(false);
                    _mapTransition.LoadingMap();
                    _maps[_mapIndex].SetActive(true);
                    _mapTransition.LoadedMap();
                    _currentMap = _maps[_mapIndex];
                    _previousPosition = _player.transform.localPosition;
                    _player.transform.localPosition = Vector3.zero;
                    _mapIndex++;
                    if (_mapIndex == _maps.Length)
                        isFinalMain = true;
                    break;
                case PortalType.PREVIOUS:
                    if ((_mapIndex - 2) < 0)
                    {
                        //던전 클리어 혹은 탈출장치 발견 전까지 나갈 수 없다는 팝업 표시하면 좋을 것 같습니다.
                        Debug.Log("들어올 땐 마음대로지만 나갈 땐 아니란다...");
                        break;
                    }
                    _currentMap?.SetActive(false);
                    _mapTransition.LoadingMap();
                    _maps[_mapIndex - 2].SetActive(true);
                    _player.transform.localPosition = _previousPosition;
                    _mapTransition.LoadedMap();
                    _currentMap = _maps[_mapIndex - 2];
                    _mapIndex--;
                    break;
                case PortalType.SIDE:
                    _currentMap?.SetActive(false);
                    _mapTransition.LoadingMap();
                    if (_mapIndex == _maps.Length)
                        _sideMaps[_mapIndex - 1].SetActive(true);
                    else _sideMaps[_mapIndex].SetActive(true);
                    _mapTransition.LoadedMap();
                    _currentMap = _sideMaps[_mapIndex];
                    _previousPosition = _player.transform.localPosition;
                    _player.transform.localPosition = Vector3.zero;
                    _mapIndex++;
                    break;
                case PortalType.BOSS:
                    _currentMap?.SetActive(false);
                    _mapTransition.LoadingMap();
                    _bossMap.SetActive(true);
                    _mapTransition.LoadedMap();
                    _currentMap = _bossMap;
                    _previousPosition = _player.transform.localPosition;
                    _player.transform.localPosition = Vector3.zero;
                    break;
            }
        }
        catch (Exception ex)
        {
            _currentMap?.SetActive(false);
            _mapTransition.LoadingMap();
            _bossMap.SetActive(true);
            _mapTransition.LoadedMap();
            _currentMap = _bossMap;
            _previousPosition = _player.transform.localPosition;
            _player.transform.localPosition = Vector3.zero;
        }
       
    }

    public void ReadyNextMap(Define.PortalType type)
    {
        _playerController.isNearPortal = true;
        _playerController.portalType = type;
    }

    public void PortalAway()
    {
        _playerController.isNearPortal = false;
    }

    public void MovedNextMap()
    {
        _playerController.isNearPortal = false;
    }
}
