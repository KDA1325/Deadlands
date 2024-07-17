using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoldManager
{
    Text _goldText;
    int _currentGold;
    int _inGameGold;

    public int CurrentGold { get { return _currentGold; } }
    public int InGameGold { get { return _inGameGold;} }

    public void Init(GoldText goldText)
    {
        _goldText = goldText.GetComponent<Text>();
        _currentGold = (int)Managers.Data.Stat[(int)Define.SaveFile.Gold];
        _inGameGold = 0;

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "MainScene")
        {
            Debug.Log("MainScene");
            _goldText.text = Managers.UI.GetNumString(_currentGold);
        }
        else if (currentSceneName == "GameScene")
        {
            Debug.Log("GameScene");
            _goldText.text = Managers.UI.GetNumString(_inGameGold);
        }
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
    public int InGameEarnGold(int amount)
    {
        if (_inGameGold < 0)
            _inGameGold = 0;

        if (amount < 0)
            return InGameGold;

        _inGameGold += amount;
        _inGameGold = Mathf.Min(_inGameGold, 100000000);
        _goldText.text = Managers.UI.GetNumString(_inGameGold);

        return InGameGold;
    }
    public int StolenGold(int amount)
    {
        if (_inGameGold < 0)
            _inGameGold = 0;

        if (amount < 0)
            return InGameGold;

        _inGameGold -= amount;
        _inGameGold = Mathf.Max(_inGameGold, 0);
        _goldText.text = Managers.UI.GetNumString(_inGameGold);

        return InGameGold;
    }

    public void Clear()
    {
        _goldText = null;
    }
}
