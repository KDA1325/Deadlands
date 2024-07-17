using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Bounce _playerBounce;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/bounce");
        _playerBounce = _player.GetComponent<PCStat_Bounce>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerBounce.BouncePer = (int)enforce[_level][0];
        _playerBounce.BounceDecrease = enforce[_level][1];
        _playerBounce.BounceMax = (int)enforce[_level][2];
    }
}