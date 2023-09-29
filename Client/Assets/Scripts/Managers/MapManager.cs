using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _maps;
    [SerializeField] private GameObject _startMap;
    [SerializeField] private GameObject _mapTransitionImage;
    private TestPlayerMovement _playerController;
    private GameObject _currentMap = null;
    private MapTransition _mapTransition;
    private int _mapIndex = 0;
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

    }

    public void LoadNextMap(PortalType type) //TODO : �������� �� ����
    {
        switch(type)
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
                break;
            case PortalType.PREVIOUS:
                if((_mapIndex -2)<0)
                {
                    //���� Ŭ���� Ȥ�� Ż����ġ �߰� ������ ���� �� ���ٴ� �˾� ǥ���ϸ� ���� �� �����ϴ�.
                    Debug.Log("���� �� ����������� ���� �� �ƴ϶���...");
                    break;
                }
                _currentMap?.SetActive(false);
                _mapTransition.LoadingMap();
                _maps[_mapIndex-2].SetActive(true);
                _player.transform.localPosition = _previousPosition;
                _mapTransition.LoadedMap();
                _currentMap = _maps[_mapIndex-2];
                _mapIndex--;
                break;
        }
    }

    public void ReadyNextMap(PortalType type)
    {
        _playerController.isNearPortal = true;
        _playerController.portalType = type;
    }

    public void MovedNextMap()
    {
        _playerController.isNearPortal = false;
    }
}
