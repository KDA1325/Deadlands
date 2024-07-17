using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Gold : PCStat_Base
{
    float _originGold = 0;

    [SerializeField]
    float _additionalCoinObtain;
    [SerializeField]
    int _coinObtainPercent;
    [SerializeField]
    int _coinObtainRatio;

    public override void Init()
    {
        _coinObtainPercent = (int)GetOutGameStat(Define.OutGameStat.CoinObtainPercent);
        _coinObtainRatio = (int)GetOutGameStat(Define.OutGameStat.CoinObtainRatio);
        _originGold = Managers.Gold.CurrentGold;
    }

    public float GetEarnGold()
    {
        float currentGold = Managers.Gold.CurrentGold;
        return Mathf.Max(0, currentGold - _originGold);
    }

    public void SetAdditionalCoinObtain(float additionalCoinObtain)
    {
        _additionalCoinObtain = additionalCoinObtain;
    }

    public void KillCompensation()
    {
        if (Random.Range(0, 100) < _coinObtainPercent)
            Managers.Gold.EarnGold(_coinObtainRatio + (int)(_coinObtainRatio * _additionalCoinObtain));
    }
}