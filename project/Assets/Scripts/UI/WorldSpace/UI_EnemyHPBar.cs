using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHPBar : UI_Base
{
    enum GameObjects
    {
        HPSlider
    }

    Slider _slider;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        _slider = Get<GameObject>((int)GameObjects.HPSlider).GetComponent<Slider>();
    }

    public void SetHP(float currentHp, float maxHp)
    {
        if (maxHp <= 0)
            return;

        _slider.value = currentHp / maxHp;
    }
}
