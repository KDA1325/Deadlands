using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.Scene.SetScene(Define.Scene.GameScene, this);

        Time.timeScale = 1.0f;

        UI_Game _ui = FindAnyObjectByType<UI_Game>();
        if (_ui == null)
        {
            Managers.UI.SetSceneUI<UI_Game>();
        }
        
        Managers.Sound.Play("Medieval Music Pack/Medieval Vol. 2 6", Define.Sound.Bgm);
    }

    public override void Clear()
    {
        ObjectHandler.Clear();
    }
}
