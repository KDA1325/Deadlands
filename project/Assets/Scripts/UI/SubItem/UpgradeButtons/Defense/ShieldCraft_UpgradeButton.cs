using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCraft_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Shield _shield;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/defenseSkills/shieldCraft");
        _shield = _player.GetComponent<PCStat_Shield>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _shield.Shild_Craft_Per = enforce[_level][0];
        _shield.Shild_Craft_Sec = enforce[_level][1];
    }
}
