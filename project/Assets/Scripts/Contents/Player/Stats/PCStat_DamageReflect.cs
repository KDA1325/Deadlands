using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_DamageReflect : PCStat_Base
{
    [SerializeField]
    float _damageReflectPer;

    public float Damage_ReflectPer { set { _damageReflectPer = value; } }

    public override void Init()
    {
        _damageReflectPer = (int)GetOutGameStat(Define.OutGameStat.PlayerReflectPercent);
    }

    public bool GetReflectWhether()
    {
        return _damageReflectPer > Random.Range(0, (float)100);
    }
}
