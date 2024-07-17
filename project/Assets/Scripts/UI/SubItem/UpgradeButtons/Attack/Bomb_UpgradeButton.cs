using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_UpgradeButton : Max_UpgradeButton
{
    Dictionary<int, List<float>> enforce = new Dictionary<int, List<float>>();
    PCStat_Bomb _playerBomb; 

    public override void Init()
    {
        base.Init();

        enforce = Managers.Data.GetDataFile("Skills/attackSkills/bomb");
        _playerBomb = _player.GetComponent<PCStat_Bomb>();

        MaxLevel = 6;
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        _playerBomb.BombPer = (int)enforce[_level][0];
        _playerBomb.BombRange = enforce[_level][1];
        _playerBomb.BombDamage += enforce[_level][2];
    }
}