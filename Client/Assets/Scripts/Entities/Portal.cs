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
            //���� ������ �̵� �����ϴٴ� UI ǥ���ϸ� ���� �� �����ϴ�
            //�� �Ŵ������� �÷��̾ �̵� ������ ���·� �����޶�� �մϴ�.
            Debug.Log($"{type}���� �̵� ����!");
            MapManager.Instance.ReadyNextMap(type);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("��Ż�� �־�����");
        MapManager.Instance.PortalAway();
    }
}
