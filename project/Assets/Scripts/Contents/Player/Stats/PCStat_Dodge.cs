using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Dodge : PCStat_Base
{
    [SerializeField]
    float _dodgePer;

    public float DodgePer { get { return _dodgePer; } set { _dodgePer = value; } }

    public override void Init()
    {
        DodgePer += GetOutGameStat(Define.OutGameStat.PlayerEnvasionPercent);
    }

    public bool GetDodgeWhether()
    {
        return DodgePer > Random.Range(0, (float)100);
    }
}
