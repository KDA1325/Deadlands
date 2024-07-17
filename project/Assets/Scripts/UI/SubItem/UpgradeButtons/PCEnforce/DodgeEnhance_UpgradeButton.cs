using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnhance_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Dodge _playerDodge;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/pcSkills/dodgeEnhance");
        _playerDodge = _player.GetComponent<PCStat_Dodge>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerDodge.DodgePer = enforce[_level][0];
    }
}