using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PCStat_Base : MonoBehaviour
{
    protected Dictionary<int, List<float>> inGameStat;
    
    private void Awake()
    {
        inGameStat = Managers.Data.GetDataFile("InGameFile/PlayerInGameStat");

        Init();
    }

    public abstract void Init();

    protected float GetOutGameStat(Define.OutGameStat _type)
    {
        float baseStat = Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat")[(int)_type][0];
        float upgradeStat = Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat")[(int)_type][1];
        int upgradeTime = (int)Managers.Data.Stat[(int)_type];

        for (int i = 0; i < upgradeTime; ++i)
        {
            baseStat += Mathf.Round(upgradeStat * 100f) / 100f;
        }

        return Mathf.Round(baseStat * 100) / 100;
    }
}