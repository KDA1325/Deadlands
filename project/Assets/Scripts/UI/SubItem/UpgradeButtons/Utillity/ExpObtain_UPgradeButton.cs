using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpObtain_UPgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Level _playerLevel;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/utilitySkills/additionalExpObtain");
        _playerLevel = _player.GetComponent<PCStat_Level>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerLevel.SetAdditionalExpObtail(enforce[_level][0]);
    }
}
