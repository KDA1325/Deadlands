using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Defense : PCStat_Base
{
    [SerializeField]
    float _defense;

    public float Defense { get { return _defense; } set { _defense = value; } }

    public override void Init()
    {
        Defense = inGameStat[(int)Define.PlayerInGameStat.Defense][0];
    }
}
