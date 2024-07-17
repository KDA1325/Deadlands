using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Damage : PCStat_Base
{
    [SerializeField]
    float _attDamage;

    [SerializeField]
    int _criPer;
    [SerializeField]
    float _criDamage;

    public float Att_Damage { get { return _attDamage; } set { _attDamage = value; } }

    int CriPercent { get { return _criPer; } set { _criPer = value; } }
    public float CriDamage { get { return _criDamage; } set { _criDamage = value; } }

    public override void Init()
    {
        Att_Damage = GetOutGameStat(Define.OutGameStat.PlayerAttDamage);

        CriPercent = (int)GetOutGameStat(Define.OutGameStat.PlayerCriPercent);
        CriDamage = GetOutGameStat(Define.OutGameStat.PlayerCriDamage);
    }

    public float GetDamage()
    {
        float damage = Att_Damage;

        if (Random.Range(0, 100) < CriPercent)
            damage *= CriDamage;

        return damage;
    }
}
