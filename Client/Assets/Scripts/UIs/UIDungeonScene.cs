using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDungeonScene : MonoBehaviour
{
    
    // UIManager µî·Ï¿ë
    private void Awake()
    {
        UIManager.Instance.RegistUI(GetComponentInChildren<UIDungeonResult>(true));
    }
}
