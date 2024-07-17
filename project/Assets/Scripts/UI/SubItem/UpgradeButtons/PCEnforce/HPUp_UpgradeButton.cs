using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUp_UpgradeButton : NoMax_UpgradeButton
{
    float enforce = 0;
    PCStat_HP _playerHp;

    public override void Init()
    {
        base.Init();

        Dictionary<int, List<float>> stat = Managers.Data.GetDataFile("Skills/pcSkills/hpUp");
        enforce = stat[1][0];

        _playerHp = _player.GetComponent<PCStat_HP>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerHp.MaxHP *= enforce;
        _playerHp.HP += _playerHp.MaxHP * enforce - _playerHp.MaxHP;
    }
}
