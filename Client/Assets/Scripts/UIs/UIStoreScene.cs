using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoreScene : MonoBehaviour
{
    // store 씬 자체가 전부 UI인데 프리팹화 할 필요가 없다고 생각해서 직접 UIManager에 등록...
    private void Awake()
    {
        for (int i = 1; i <= 4; i++)
        {
            UIManager.Instance.RegistUI(transform.GetChild(i).GetComponent<UIBase>());
        }
    }
}
