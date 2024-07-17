using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager
{
    Text _goldText;
    int _currentGold;

    public int CurrentGold { get { return _currentGold; } }

    public void Init(GoldText goldText)
    {
        _goldText = goldText.GetComponent<Text>();
        _currentGold = (int)Managers.Data.Stat[(int)Define.SaveFile.Gold];

        _goldText.text = Managers.UI.GetNumString(_currentGold);
    }

    public int UseGold(int amount)
    {
        if (_currentGold < 0)
            _currentGold = 0;

        if (amount < 0)
            return CurrentGold;

        _currentGold -= amount;
        _currentGold = Mathf.Max(_currentGold, 0);
        _goldText.text = Managers.UI.GetNumString(_currentGold);
        Managers.Data.Stat[(int)Define.SaveFile.Gold] = _currentGold;
        Managers.Data.SetSaveFile();

        return CurrentGold;
    }
    public int EarnGold(int amount)
    {
        if (_currentGold < 0)
            _currentGold = 0;

        if (amount < 0)
            return CurrentGold;

        _currentGold += amount;
        _currentGold = Mathf.Min(_currentGold, 100000000);
        _goldText.text = Managers.UI.GetNumString(_currentGold);
        Managers.Data.Stat[(int)Define.SaveFile.Gold] = _currentGold;
        Managers.Data.SetSaveFile();

        return CurrentGold;
    }

    public void Clear()
    {
        _goldText = null;
    }
}
