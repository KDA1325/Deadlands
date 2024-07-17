using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Resurrection : PCStat_Base
{
    [SerializeField]
    bool _resurrection;
    [SerializeField]
    float _resurrectionHP;
    [SerializeField]
    int _playerResurrectionPercent;

    PCStat_HP _hp;

    public override void Init()
    {
        _hp = GetComponent<PCStat_HP>();

        _playerResurrectionPercent = (int)GetOutGameStat(Define.OutGameStat.PlayerResurrectionPercent);
    }

    public void SetResurrection(bool resurrection, float resurrectionHP)
    {
        _resurrection = resurrection;
        _resurrectionHP = resurrectionHP;
    }

    public bool TryResurrection()
    {
        if (_resurrection)
        {
            _resurrection = false;
            
            StartCoroutine("OnResurrection");
            return true;
        }
        else if (_playerResurrectionPercent > UnityEngine.Random.Range(0, 100))
        {
            StartCoroutine("OnResurrection");
            return true;
        }

        return false;
    }

    IEnumerator OnResurrection()
    {
        GetComponent<PCStat_PlaySpeed>().GamePause();
        yield return new WaitForSecondsRealtime(1);
        _hp.HP = _hp.MaxHP * _resurrectionHP;
        GetComponent<PCStat_PlaySpeed>().GamePlay();
        ObjectHandler.Spawn("Resurrection", Define.Layer.Effect).transform.SetParent(transform);
    }
}
