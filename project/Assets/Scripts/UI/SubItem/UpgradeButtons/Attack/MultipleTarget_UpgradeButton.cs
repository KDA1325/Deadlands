using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTarget_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_MultipleTarget _playerMultipleTarget;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/additionalMultipleTarget");
        _playerMultipleTarget = _player.GetComponent<PCStat_MultipleTarget>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerMultipleTarget._multipleTarget = (int)enforce[_level][0];
    }
}
