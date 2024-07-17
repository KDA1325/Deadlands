using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    protected Canvas canvas;
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        canvas = GetComponent<Canvas>();
    }

    // 자식 UI오브젝트를 코드에 저장하는 함수
    protected void Bind<T>(Type _type) where T : UnityEngine.Object
    {
        if (_objects.ContainsKey(typeof(T)))
            return;

        string[] names = Enum.GetNames(_type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; ++i)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }
        }
    }

    // 가져온 자식 UI를 불러오는 함수
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            Debug.Log($"UI_Base : Get faild : {objects[idx].name}");
            return null;
        }

        return objects[idx] as T;
    }

    public virtual void SetInfo(int _order)
    {
        canvas.sortingOrder = _order;
    }
}