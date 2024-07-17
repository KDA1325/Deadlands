using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_AttSpeed : PCStat_Base
{
    [SerializeField]
    float _attSpeed;

    public float Att_Speed
    {
        get { return _attSpeed; }
        set
        {
            _attSpeed = value;
        }
    }

    public override void Init()
    {
        Att_Speed = GetOutGameStat(Define.OutGameStat.PlayerAttSpeed);
    }
}
