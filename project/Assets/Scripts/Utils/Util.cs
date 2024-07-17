using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    public static string GetObjectTypeName(Define.ObjectType type)
    {
        return type.ToString();
    }

    public static T FindChild<T>(GameObject go, string name, bool recursive = false) where T : UnityEngine.Object
    {
        if (recursive)
        {
            foreach (T child in go.GetComponentsInChildren<T>())
            {
                if (child.name == name)
                {
                    return child;
                }
            }
        }
        else
        {
            return go.transform.Find(name).GetComponent<T>();
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform != null)
        {
            return transform.gameObject;
        }

        return null;
    }

    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }
}
