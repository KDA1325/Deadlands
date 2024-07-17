using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBullet_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Split _playerSplit;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/splitBullet");
        _playerSplit = _player.GetComponent<PCStat_Split>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerSplit.Split_BulletPer = (int)enforce[_level][0];
        _playerSplit.Split_BulletDecrease = (int)enforce[_level][1];
    }
}