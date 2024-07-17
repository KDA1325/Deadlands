using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Gold : PCStat_Base
{
    float _originGold = 0;
    int _inGameGold = 0;

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
        _inGameGold = Managers.Gold.InGameGold;
    }

    public float GetEarnGold()
    {
        float currentGold = Managers.Gold.InGameGold;
        return currentGold;
        //return Mathf.Max(0, currentGold - _inGameGold);
    }

    public void SetAdditionalCoinObtain(float additionalCoinObtain)
    {
        _additionalCoinObtain = additionalCoinObtain;
    }

    public void KillCompensation()
    {
        if (Random.Range(0, 100) < _coinObtainPercent)
            Managers.Gold.InGameEarnGold(_coinObtainRatio + (int)(_coinObtainRatio * _additionalCoinObtain));
    }
}