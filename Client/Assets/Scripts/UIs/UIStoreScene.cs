using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoreScene : MonoBehaviour
{
    // store �� ��ü�� ���� UI�ε� ������ȭ �� �ʿ䰡 ���ٰ� �����ؼ� ���� UIManager�� ���...
    private void Awake()
    {
        for (int i = 1; i <= 4; i++)
        {
            UIManager.Instance.RegistUI(transform.GetChild(i).GetComponent<UIBase>());
        }
    }
}
