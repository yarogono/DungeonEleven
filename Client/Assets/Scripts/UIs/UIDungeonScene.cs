using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDungeonScene : MonoBehaviour
{
    
    // UIManager ��Ͽ�
    private void Awake()
    {
        UIManager.Instance.RegistUI(GetComponentInChildren<UIDungeonResult>(true));
    }
}
