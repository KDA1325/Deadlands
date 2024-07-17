using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShot_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_DualShot _playerDualShot;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/dualShot");
        _playerDualShot = _player.GetComponent<PCStat_DualShot>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerDualShot.DualShotPer += (int)enforce[_level][0];
    }
}
