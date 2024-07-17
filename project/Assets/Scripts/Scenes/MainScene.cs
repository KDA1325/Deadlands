using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        Managers.Scene.SetScene(Define.Scene.MainScene, this);

        GameObject ui = GameObject.Find("UI_Main");
        if (ui == null)
        {
            Managers.UI.SetSceneUI<UI_Main>();
        }
        
        Managers.Sound.Play("Medieval Music Pack/Medieval Vol. 2 1", Define.Sound.Bgm);
        
        base.Init();
    }

    public override void Clear()
    {

    }
}