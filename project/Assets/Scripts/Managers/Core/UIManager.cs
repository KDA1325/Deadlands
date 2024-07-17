using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    int _idx = 100;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    Stack<UI_Popup> _popup = new Stack<UI_Popup>();

    public void Init()
    {

    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup // 제한
    {
        if (name == null)
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = go.GetComponent<T>();

        _popup.Push(popup);
        popup.SetInfo(_idx++);

        popup.transform.SetParent(Root.transform, false);

        return popup;
    }
    public UI_Scene SetSceneUI<T>(string path = null, string name = null) where T : UI_Scene // 제한
    {
        if (name == null)
            name = typeof(T).Name;

        if (path == null)
            path = name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{path}");
        T scene = go.GetComponent<T>();

        scene.SetInfo(1);

        return scene;
    }
    public UI_Base MakeSubItem<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (name == null)
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}", parent);

        T item = go.GetComponent<T>();

        return item;
    }
    public UI_Base MakeWorldSpace<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (name == null)
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}", parent);

        T item = go.GetComponent<T>();

        return item;
    }

    public void ClosePopupUI()
    {
        if (_popup.Count <= 0)
        {
            return;
        }

        UI_Popup popup = _popup.Pop();

        popup.Clear();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        --_idx;
    }
    public void CloaseAllPopupUI()
    {
        while (_popup.Count > 0)
        {
            ClosePopupUI();
        }
    }

    public void Clear()
    {
        CloaseAllPopupUI();
        _popup.Clear();
    }

    // UI 텍스트 변환
    public void ChangeUIText(Text text, string content)
    {
        text.text = content;
    }
    public void ChangeUIText(Text text, float content)
    {
        text.text = GetNumString(content);
    }
    public string GetNumString(float num)
    {
        if (num >= 1000000000)
        {
            num /= 100000000;
            num = Mathf.Floor(num) / 10f;

            return $"{num}b";
        }
        else if (num >= 1000000)
        {
            num /= 100000;
            num = Mathf.Floor(num) / 10f;

            return $"{num}m";
        }
        else if (num >= 1000)
        {
            num /= 100;
            num = Mathf.Floor(num) / 10f;

            return $"{num}k";
        }
        else
        {
            return num.ToString();
        }
    }
}