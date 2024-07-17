using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_MultipleTarget : PCStat_Base
{
    public int _multipleTarget;
    public int _multiShotPer;
    public int _multiShotCount = 2;

    public override void Init()
    {
        _multiShotPer = (int)GetOutGameStat(Define.OutGameStat.MultiShotPercent);
        _multiShotCount = (int)GetOutGameStat(Define.OutGameStat.MultiShotCount);
    }

    public int GetMultipleTargetCount()
    {
        int multipleTarget = _multipleTarget;

        if (_multiShotPer > UnityEngine.Random.Range(0, 100))
            multipleTarget += _multiShotCount;

        return multipleTarget;
    }
}
