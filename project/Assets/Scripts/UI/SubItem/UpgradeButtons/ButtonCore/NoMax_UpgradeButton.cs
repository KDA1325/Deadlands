using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoMax_UpgradeButton : Base_UpgradeButton
{
    enum Texts
    {
        SkillLevelText
    }

    Text _levelText;

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));
        _levelText = Get<Text>((int)Texts.SkillLevelText);
    }

    private void OnEnable()
    {
        _levelText.text = $"LEVEL {_level}";
    }
}
