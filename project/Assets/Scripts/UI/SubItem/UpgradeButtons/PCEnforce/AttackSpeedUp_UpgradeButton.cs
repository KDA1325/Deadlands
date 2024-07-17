using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp_UpgradeButton : NoMax_UpgradeButton
{
    float enforce = 0;
    PCStat_AttSpeed _playerAttSpeed;

    public override void Init()
    {
        base.Init();

        Dictionary<int, List<float>> stat = Managers.Data.GetDataFile("Skills/pcSkills/attackSpeedUp");
        enforce = stat[1][0];

        _playerAttSpeed = _player.GetComponent<PCStat_AttSpeed>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerAttSpeed.Att_Speed *= enforce;
    }
}
