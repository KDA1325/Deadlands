using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class SpawningPool : MonoBehaviour
{
    enum EnemySpawnPer
    {
        SpawnPer,
        ChangeSpawnPer,
    }
    enum EnemyCall
    {
        EnemyPopulation,
        EnemyPopulationChange,
        EnemyCallCycle,
        EnemyCallCycleChange,
    }

    #region Spawn
    [Header("스폰 범위 및 시간 설정")]
    [SerializeField]
    Vector2 _spawnPos;
    [SerializeField]
    float _spawnRadius = 15;
    [SerializeField]
    float _spawnTime = 1;
    #endregion

    #region Wave
    [Header("웨이브 스탯")]
    [SerializeField]
    int _currentWave = 1;
    [SerializeField]
    int _targetWave = 50;
    [SerializeField]
    int _currentKill = 0;
    [SerializeField]
    int _targetKill = 20;

    public int CurrentWave
    {
        get { return Mathf.Max(1, _currentWave); }
        set
        {
            _currentWave = value;

            // 웨이브 클리어
            if (OnWaveClear != null)
                OnWaveClear.Invoke(_currentWave);

            // 승리
            if (_currentWave == TargetWave + 1)
            {
                Managers.UI.ShowPopupUI<UI_Clear>();

                if (Managers.Scene.CurrentStage > Managers.Data.Stat[(int)Define.SaveFile.ClearStage])
                {
                    Managers.Data.Stat[(int)Define.SaveFile.ClearStage] = Managers.Scene.CurrentStage;
                }

                ObjectHandler.Player.GetComponent<PCStat_PlaySpeed>().GamePause();
            }


            for (int i = 0; i < _enemySpawnPer.Count; ++i)
            {
                _enemySpawnPer[i][(int)EnemySpawnPer.SpawnPer] += _enemySpawnPer[i][(int)EnemySpawnPer.ChangeSpawnPer];
            }

            _spawnTime += _enemyCall[(int)EnemyCall.EnemyCallCycleChange];

            if (_currentWave % 5 == 0)
            {
                _targetKill += 1;
            }

            if (_currentWave % 20 == 0)
            {
                ++_enemyLevel;
            }

            if (_currentWave % 10 == 0)
            {
                SpawnEnemy(Define.EnemyType.D);
            }
        }
    }
    public int TargetWave { get { return _targetWave; } }
    public int CurrentKill
    {
        get { return _currentKill; }
        set
        {
            _currentKill = value;
            if (_currentKill >= _targetKill)
            {
                ++CurrentWave;
                _currentKill = 0;
            }
        }
    }
    public int TargetKill { get { return _targetKill; } set { _targetKill = value; } }

    public Action<int> OnWaveClear = null;
    #endregion

    List<float[]> _enemySpawnPer = new List<float[]>();
    [SerializeField]
    List<float> _enemyCall;
    [SerializeField]
    int _enemyLevel = 1;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _targetWave = Managers.Scene.CurrentStage * 50;

        Dictionary<int, List<float>> enemyStat = Managers.Data.GetDataFile($"StageFile/{Managers.Scene.CurrentStage}stage");

        // 소환 확률
        for (int i = 0; i < enemyStat.Count - 1; i++)
        {
            _enemySpawnPer.Add(enemyStat[i].ToArray());
        }

        // 소환 수, 속도
        _enemyCall = enemyStat[enemyStat.Count - 1];
        _spawnTime = _enemyCall[(int)EnemyCall.EnemyCallCycle];

        StartCoroutine("SpawnTime");
    }

    IEnumerator SpawnTime()
    {
        while (true)
        {
            SpawnEnemy(GetRandomEnemyType());

            yield return new WaitForSeconds(_spawnTime);
        }
    }

    void SpawnEnemy(Define.EnemyType enemyType)
    {
        GameObject go = ObjectHandler.Spawn($"Enemy");

        EnemyStat stat = go.GetComponent<EnemyStat>();
        stat.SpawnInit(CurrentWave, enemyType);

        go.transform.position = GetRandomSpawnPoint();
    }

    Define.EnemyType GetRandomEnemyType()
    {
        float rand = UnityEngine.Random.Range(0, (float)100);

        float temp = 0;

        for (int i = 0; i < _enemySpawnPer.Count; ++i)
        {
            temp += _enemySpawnPer[i][(int)EnemySpawnPer.SpawnPer];
            if (rand <= temp)
                return (Define.EnemyType)i + 1;
        }

        return Define.EnemyType.A;
    }

    Vector2 GetRandomSpawnPoint()
    {
        Vector2 randPos;

        Vector2 randDir = UnityEngine.Random.insideUnitCircle.normalized;
        randDir *= _spawnRadius;
        randPos = _spawnPos + randDir;

        return randPos;
    }

    void KillAllMonster(int level)
    {
        foreach (EnemyStat enemy in transform.GetComponentsInChildren<EnemyStat>())
        {
            if (enemy.gameObject.activeSelf != true)
                return;

            ObjectHandler.Despawn(enemy.gameObject);
            _currentKill = 0;
        }
    }
}