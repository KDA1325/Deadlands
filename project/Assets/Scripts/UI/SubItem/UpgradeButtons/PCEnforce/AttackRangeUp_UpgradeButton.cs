using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeUp_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_AttRange _playerAttRange;
    
    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/pcSkills/attackRangeUp");
        _playerAttRange = _player.GetComponent<PCStat_AttRange>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerAttRange.Att_Range += enforce[_level][0];
    }
}
