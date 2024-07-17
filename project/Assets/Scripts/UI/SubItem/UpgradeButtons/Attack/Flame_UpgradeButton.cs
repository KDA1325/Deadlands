using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Flame _playerFlame; 

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/flame");
        _playerFlame = _player.GetComponent<PCStat_Flame>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerFlame.FlamePer = (int)enforce[_level][0];
        _playerFlame.FlameDamage += enforce[_level][1];
    }
}