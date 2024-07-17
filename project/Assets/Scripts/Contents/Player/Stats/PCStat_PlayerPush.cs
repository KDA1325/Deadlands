using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_PlayerPush : PCStat_Base
{
    public int _playerPushPercent;

    public override void Init()
    {
        _playerPushPercent = (int)GetOutGameStat(Define.OutGameStat.PlayerReflectPercent);
    }

    public void SetPlayerPushPercent(int playerPushPercent)
    {
        _playerPushPercent = playerPushPercent;
    }

    public void TryPush(EnemyCtrl_Locomotion enemy)
    {
        if (enemy.gameObject.activeSelf && Random.Range(0, 100) < _playerPushPercent)
            enemy.IsPushed();
    }
}
