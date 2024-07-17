using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrain_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_LifeDrain _playerLifeDrain;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/lifeDrain");

        _playerLifeDrain = _player.GetComponent<PCStat_LifeDrain>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerLifeDrain.SetLifeDrainPer((int)enforce[_level][0]);
    }
}
