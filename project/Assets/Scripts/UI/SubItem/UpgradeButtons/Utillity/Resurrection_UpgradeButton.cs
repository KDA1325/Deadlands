using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrection_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Resurrection _playerResurrection;

    public override void Init()
    {
        MaxLevel = 1;

        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/utilitySkills/resurrection");
        _playerResurrection = _player.GetComponent<PCStat_Resurrection>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerResurrection.SetResurrection(true, enforce[_level][0]);
    }
}
