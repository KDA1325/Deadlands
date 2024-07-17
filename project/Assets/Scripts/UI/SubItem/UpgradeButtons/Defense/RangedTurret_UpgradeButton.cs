using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTurret_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_RangedTurret _rangedTurret;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/defenseSkills/rangedTurret");
        _rangedTurret = _player.GetComponent<PCStat_RangedTurret>();

        _rangedTurret._isTurretActive = false;
        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();        

        _rangedTurret.AttackDamage = enforce[_level][0];
        _rangedTurret.AttackRange = enforce[_level][1];
        _rangedTurret.RotationSpeed = enforce[_level][2];

        if(!_rangedTurret._isTurretActive)
        {
            _rangedTurret.TurretSpawn();
        }
    }
}
