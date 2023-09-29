using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortalType
{
    MAIN,
    PREVIOUS,
    SIDE,
    BOSS
}

public class Portal : MonoBehaviour
{
    [SerializeField] PortalType type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(MapManager.Instance.isFinalMain&&type== PortalType.MAIN)
            type = PortalType.BOSS;
        if (collision.CompareTag("Player"))
        {
            //다음 맵으로 이동 가능하다는 UI 표시하면 좋을 것 같습니다
            //맵 매니저에게 플레이어를 이동 가능한 상태로 만들어달라고 합니다.
            Debug.Log($"{type}으로 이동 가능!");
            MapManager.Instance.ReadyNextMap(type);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("포탈이 멀어졌다");
        MapManager.Instance.PortalAway();
    }
}
