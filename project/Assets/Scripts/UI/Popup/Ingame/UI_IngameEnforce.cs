using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_IngameEnforce : UI_Popup
{
    enum GameObjects
    {
        BoxPannel
    }

    enum Images
    {
        UpgradeImage
    }

    GameObject _boxPannel;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        _boxPannel = Get<GameObject>((int)GameObjects.BoxPannel);
    }

    private void OnEnable()
    {
        for (int i = 0; i < 3; ++i)
        {
            ObjectHandler.Player.GetComponent<PCCtrl_Skill>().SetRandomUpgradeButton(_boxPannel.transform);
        }
    }

    public override void Clear()
    {
        base.Clear();

        while (_boxPannel.transform.childCount > 0)
        {
            ObjectHandler.Player.GetComponent<PCCtrl_Skill>().ReturnUpgradeButton();
            
        }
    }
}
