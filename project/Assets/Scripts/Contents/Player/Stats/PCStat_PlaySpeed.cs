using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCStat_PlaySpeed : PCStat_Base
{
    [SerializeField]
    float _maxPlaySpeed;
    [SerializeField]
    float _currentPlaySpeed = 1;

    Text _playSpeedText;

    public override void Init()
    {
        _maxPlaySpeed = GetOutGameStat(Define.OutGameStat.PlaySpeedUp);
    }

    public float GetCurrentPlaySpeed() { return _currentPlaySpeed; }


    public void SetPlaySpeedText(Text playSpeedText)
    {
        _playSpeedText = playSpeedText;
    }

    public void SpeedUp()
    {
        if (_currentPlaySpeed < _maxPlaySpeed)
        {
            _currentPlaySpeed += 0.5f;
        }
        
        Time.timeScale = _currentPlaySpeed;
        _playSpeedText.text = _currentPlaySpeed.ToString();
    }
    public void SpeedDown()
    {
        if (_currentPlaySpeed > 0.5f)
        {
            _currentPlaySpeed -= 0.5f;
        }

        Time.timeScale = _currentPlaySpeed;
        _playSpeedText.text = _currentPlaySpeed.ToString();
    }

    public void GamePause()
    {
        Time.timeScale = 0;
    }
    public void GamePlay()
    {
        Time.timeScale = _currentPlaySpeed;
    }
}
