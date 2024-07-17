using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnhance_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Defense _playerDefense;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/pcSkills/defenseEnhance");
        _playerDefense = _player.GetComponent<PCStat_Defense>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerDefense.Defense = enforce[_level][0];
    }
}