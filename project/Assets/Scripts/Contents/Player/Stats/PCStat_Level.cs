using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCStat_Level : PCStat_Base
{
    // exp, gold
    [SerializeField]
    int _level;
    [SerializeField]
    float _currentExp;
    [SerializeField]
    int _maxExp;
    [SerializeField]
    float _additionalExpObtain;
    int _expObtainRatio;

    public Action<int> OnPlayerLevelUp = null;

    Slider _levelSlider;

    public override void Init()
    {
        _maxExp = (int)inGameStat[(int)Define.PlayerInGameStat.Max_Exp][0];
        _expObtainRatio = (int)GetOutGameStat(Define.OutGameStat.ExpObtainRatio);
    }

    public void SetLevelSlider(Slider levelSlider)
    {
        _levelSlider = levelSlider;
    }

    public void SetAdditionalExpObtail(float additionalExpObtain)
    {
        _additionalExpObtain = additionalExpObtain;
    }

    public void KillCompensation()
    {
        AddExp(_expObtainRatio + _expObtainRatio * _additionalExpObtain);
        _levelSlider.value = _currentExp / _maxExp;
    }

    void AddExp(float exp)
    {
        _currentExp += exp;

        if (_currentExp >= _maxExp)
        {
            LevelUp();
            _currentExp = 0;
        }
    }

    void LevelUp()
    {
        ++_level;

        if (OnPlayerLevelUp != null)
        {
            OnPlayerLevelUp.Invoke(_level);
        }

        if (_level % 10 == 0)
        {
            _maxExp += 20;
        }
    }
}
