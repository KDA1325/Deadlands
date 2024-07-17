using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Managers.Gold.Init(FindAnyObjectByType<GoldText>());

        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
        {
            Managers.Resource.Instantiate("UI/@EventSystem").name = "@EventSystem";
        }

        Application.targetFrameRate = 60;
    }

    public abstract void Clear();
}
