using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_AttRange : PCStat_Base
{
    [SerializeField]
    float _attRange;
    public float _originAttRange = 0.7f;

    public float Att_Range
    {
        get { return _attRange; }
        set
        {
            _attRange = value;

            AttackArea.AttRange = value * 420 * _originAttRange;
        }
    }

    UI_Att_Range AttackArea;

    public override void Init()
    {
        AttackArea = Managers.UI.MakeWorldSpace<UI_Att_Range>(transform).GetComponent<UI_Att_Range>();

        Att_Range = GetOutGameStat(Define.OutGameStat.PlayerAttRange);
    }

    public float GetRange()
    {
        return Att_Range * _originAttRange;
    }
}
