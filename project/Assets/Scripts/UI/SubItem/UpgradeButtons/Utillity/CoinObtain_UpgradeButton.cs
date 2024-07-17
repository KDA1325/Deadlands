using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObtain_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Gold _playerGold;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/utilitySkills/additionalCoinObtain");
        _playerGold = _player.GetComponent<PCStat_Gold>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerGold.SetAdditionalCoinObtain(enforce[_level][0]);
    }
}
