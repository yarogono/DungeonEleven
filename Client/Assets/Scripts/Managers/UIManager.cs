using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomSingleton<UIManager>
{
    [SerializeField] public Dictionary<string, UIBase> UIDic = new Dictionary<string, UIBase>();
    public UIBase OpenUI(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("�ش� UI�� �����ϴ�.");
            return null;
        }
        UIBase UI;
        if (UIDic.ContainsKey(name))
        {
            UI = UIDic[name];
        }
        else
        {
            UI = ResourceManager.Instance.Load<UIBase>($"Prefabs/UI/{name}");
            if (UI == null)
            {
                Debug.LogError("�ش� UI�� �����ϴ�.");
                return null;
            }
            UI = Instantiate(UI);
            UIDic.Add(name, UI);
        }

        UI.OpenUI();
        return UI;
    }
    public UIBase CloseUI(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("�ش� UI�� �����ϴ�.");
            return null;
        }
        if (!UIDic.ContainsKey(name))
        {
            Debug.LogError("�ش� UI�� �����ϴ�.");
            return null;
        }

        UIBase UI = UIDic[name];
        UI.CloseUI();
        return UI;
    }
    
    public void RegistUI(UIBase UI, string name = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            name = UI.GetType().Name;
        }
        UIDic.Add(name, UI);
    }
    public void RemoveUI(string name)
    {
        if (!UIDic.ContainsKey(name))
        {
            Debug.LogError("�ش� UI�� �����ϴ�.");
            return;
        }
        UIDic.Remove(name);
    }

    public bool IsOpenedUI(string name)
    {
        if (UIDic.ContainsKey(name))
        {
            return UIDic[name].gameObject.activeSelf;
        }
        else
        {
            Debug.LogError("�ش� UI�� �����ϴ�.");
            return false;
        }
    }

    public UIBase GetUI(string name)
    {
        if (UIDic.ContainsKey(name))
        {
            return UIDic[name];
        }
        else
        {
            Debug.LogError("�ش� UI�� �����ϴ�.");
            return null;
        }
    }
}
