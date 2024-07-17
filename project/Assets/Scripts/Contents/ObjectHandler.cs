using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ObjectHandler : MonoBehaviour
{
    static ObjectHandler _objectHandler;

    static GameObject _player;
    static SpawningPool _enemyRoot;

    public static GameObject Player
    {
        get
        {
            if (_player == null)
            {
                GameObject go = GameObject.Find("PC");

                if (go == null)
                {
                    Spawn("PC");
                }

                _player = go;
            }

            return _player;
        }
    }
    public static SpawningPool Spawner
    {
        get
        {
            if (_enemyRoot == null)
            {
                SpawningPool spawningPool = GameObject.Find("SpawningPool").GetComponent<SpawningPool>();

                if (spawningPool == null)
                {
                    GameObject go = new GameObject() { name = "SpawningPool" };
                    go.AddComponent<SpawningPool>();
                    spawningPool = go.AddComponent<SpawningPool>();
                }

                _enemyRoot = spawningPool;
            }

            return _enemyRoot;
        }
    }

    static Dictionary<string, GameObject> _root = new Dictionary<string, GameObject>();
    static Dictionary<GameObject, Queue<GameObject>> _poolQueue = new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (_objectHandler == null)
        {
            _objectHandler = this;
        }
    }

    public static GameObject Spawn(string name, Define.Layer layer = Define.Layer.Enemy)
    {
        if (layer == Define.Layer.Player)
        {
            GameObject go = Managers.Resource.Instantiate(name);
            _player = go;

            return go;
        }

        string _path = null;
        // 경로 재설정
        switch (layer)
        {
            case Define.Layer.Enemy:
                _path = $"EnemyPrefabs/{name}";
                break;
            case Define.Layer.Bullet:
                _path = $"BulletPrefabs/{name}";
                break;
            case Define.Layer.Effect:
                _path = $"EffectPrefabs/{name}";
                break;
            case Define.Layer.SingleEffect:
                _path = $"EffectPrefabs/{name}";
                break;
        }

        // root 없으면 생성
        GameObject root = null;

        if (_root.TryGetValue($"{name}_Root", out root) == false)
        {
            root = new GameObject() { name = $"{name}_Root" };
            _root.Add(root.name, root);
            root.transform.SetParent(_objectHandler.transform);

            // 안에 스택도 같이 생성
            Queue<GameObject> queue = new Queue<GameObject>();
            _poolQueue.Add(root, queue);
        }
        // 스택이 비었으면 하나 생성
        if (_poolQueue[root].Count <= 0)
        {
            GameObject go = Managers.Resource.Instantiate(_path);
            go.transform.SetParent(root.transform);

            return go;
        }

        GameObject pooledGo = _poolQueue[root].Dequeue();

        pooledGo.SetActive(true);

        return pooledGo;
    }

    public static void Despawn(GameObject go)
    {
        if (go.activeSelf == false)
        {
            return;
        }

        switch (go.layer)
        {
            case (int)Define.Layer.Player:
                {

                    return;
                }
            case (int)Define.Layer.Enemy:
                {
                    ++Spawner.CurrentKill;
                }
                break;
            case (int)Define.Layer.Bullet:
                {

                }
                break;
            case (int)Define.Layer.Effect:
                {

                }
                break;
            case (int)Define.Layer.SingleEffect:
                {

                }
                break;
        }
        go.SetActive(false);

        int idx = go.name.IndexOf("(Clone)");
        if (idx > 0)
            go.name = go.name.Substring(0, idx);

        _poolQueue[_root[$"{go.name}_Root"]].Enqueue(go);

        go.transform.position = Vector3.zero;
    }

    public static int GetEnemyCount() { return _enemyRoot.transform.childCount; }

    public static void Clear()
    {
        _root.Clear();
        foreach (GameObject go in _poolQueue.Keys)
            _poolQueue[go].Clear();
        _poolQueue.Clear();
    }
}