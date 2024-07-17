using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Freeze _playerFreeze; 

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/freeze");
        _playerFreeze = _player.GetComponent<PCStat_Freeze>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerFreeze.FreezePer = (int)enforce[_level][0];
    }
}
