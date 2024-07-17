using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Alarm : UI_Popup
{
    enum Texts
    {
        LackText
    }

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));
    }

    public void SetText(string text)
    {
        Get<Text>((int)Texts.LackText).text = text;
    }

    public void Close()
    {
        Managers.UI.ClosePopupUI();
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);
    }
}