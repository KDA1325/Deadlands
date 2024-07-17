using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemageReflect_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_DamageReflect _playerDamageReflact;

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/defenseSkills/demageReflect");
        _playerDamageReflact = _player.GetComponent<PCStat_DamageReflect>();
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerDamageReflact.Damage_ReflectPer = enforce[_level][0];
    }
}
