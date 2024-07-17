using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationBullet_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Penetration _playerPenetration;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/penetrationBullet");
        _playerPenetration = _player.GetComponent<PCStat_Penetration>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerPenetration.Penetration_Per = (int)enforce[_level][0];
        _playerPenetration.Penetration_Decrease = enforce[_level][1];
    }
}
