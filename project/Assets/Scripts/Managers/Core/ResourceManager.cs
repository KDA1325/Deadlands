using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        T obj = Resources.Load<T>(path);

        if (obj == null)
        {
            Debug.Log($"ResourceManager : Wrong path : {path}");
            return null;
        }

        return obj;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if (original == null)
        {
            Debug.Log($"Resource : Wrong path : {path}");
            return null;
        }

        return Instantiate(original, parent);
    }
    public GameObject Instantiate(string path, Vector3 position, Transform parent = null)
    {
        GameObject go = Instantiate(path, parent);
        go.transform.position = position;

        return go;
    }
    public GameObject Instantiate(GameObject go, Transform parent = null)
    {
        GameObject original = Object.Instantiate(go, parent);
        int idx = original.name.IndexOf("(Clone)");
        if (idx > 0)
            original.name = original.name.Substring(0, idx);

        return original;
    }

    public void Destroy(GameObject go, float time = -1)
    {
        if (go == null)
        {
            Debug.Log($"Resource : faild destroy to objecct {go} : null");
        }

        if (time < 0)
            GameObject.Destroy(go.gameObject, time);
        else
            GameObject.Destroy(go.gameObject);
    }
}
