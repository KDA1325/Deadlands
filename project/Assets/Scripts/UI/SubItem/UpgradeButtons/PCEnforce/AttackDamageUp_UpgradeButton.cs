using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageUp_UpgradeButton : NoMax_UpgradeButton
{
    float enforce = 0;
    PCStat_Damage _playerDamage;

    public override void Init()
    {
        base.Init();

        Dictionary<int, List<float>> stat = Managers.Data.GetDataFile("Skills/pcSkills/attackDamageUp");
        enforce = stat[1][0];
        _playerDamage = _player.GetComponent<PCStat_Damage>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerDamage.Att_Damage *= enforce;
    }
}