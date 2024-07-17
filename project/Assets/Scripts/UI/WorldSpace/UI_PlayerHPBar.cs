using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHPBar : UI_Base
{
    enum GameObjects
    {
        HPSlider
    }

    enum Texts
    {
        HPText
    }

    Slider _slider;
    Text _playerHp;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));

        _slider = Get<GameObject>((int)GameObjects.HPSlider).GetComponent<Slider>();
        _playerHp = Get<Text>((int)Texts.HPText).GetComponent<Text>();


    }

    public void SetHP(float currentHp, float maxHp)
    {
        _slider.value = currentHp / maxHp;
        Managers.UI.ChangeUIText(_playerHp, $"{currentHp} / {maxHp}");
    }
}