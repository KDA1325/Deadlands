using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_LifeDrain : PCStat_Base
{
    [SerializeField]
    int _lifeDrainPer;

    PCStat_HP _playerHP;
    public override void Init()
    {
        _lifeDrainPer = (int)GetOutGameStat(Define.OutGameStat.LifeDrainPercent);
        _playerHP = gameObject.GetOrAddComponent<PCStat_HP>();
    }

    public void SetLifeDrainPer(int lifeDrainPer)
    {
        _lifeDrainPer = lifeDrainPer;
    }

    public void TryLifeDrain()
    {
        if (_lifeDrainPer > Random.Range(0, 100))
        {
            _playerHP.HP += _playerHP.MaxHP * 0.1f;
        }
    }
}
