using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PCStat_DualShot : PCStat_Base
{
    public int _dualShotPer;
    public int _dualShotCount = 2;

    public int DualShotPer { get { return _dualShotPer; } set { _dualShotPer = value; } }
    public int DualShotCount { get { return _dualShotCount; } set { _dualShotCount = value; } }

    public override void Init()
    {
        DualShotPer = (int)GetOutGameStat(Define.OutGameStat.DualShotPercent);
        DualShotCount = (int)GetOutGameStat(Define.OutGameStat.DualShotCount);
    }

    public int GetDualShotCount()
    {
        return _dualShotPer > Random.Range(0, 100) ? _dualShotCount : 1;
    }
}
