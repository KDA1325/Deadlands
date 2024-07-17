using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardPanel : UI_Base
{
    enum GameObjects
    {
        Content,
    }

    [SerializeField]
    Define.SaveFile Type;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        UnlockInit();
    }

    void UnlockInit()
    {
        GameObject content = Get<GameObject>((int)GameObjects.Content);
        int UnlockedLevel = (int)Managers.Data.Stat[(int)Type];
        int _chackLevel = 0;

        for (int i = 0; i < content.transform.childCount; ++i)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < content.transform.childCount; ++i)
        {
            if (content.transform.GetChild(i).name == $"UnlockButton")
            {
                if (UnlockedLevel == _chackLevel)
                {
                    content.transform.GetChild(i).gameObject.SetActive(true);
                    break;
                }
                else
                {
                    ++_chackLevel;
                    continue;
                }

            }

            content.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OnUnlockButton()
    {
        int unlockLevel = (int)Managers.Data.Stat[(int)Type];
        int price = (int)Managers.Data.GetDataFile("OutGameFile/CardUnlock")[(int)Type][unlockLevel];

        if (Managers.Gold.CurrentGold < price)
        {
            Managers.UI.ShowPopupUI<UI_Alarm>().SetText("돈이 부족합니다");

            return;
        }

        Managers.Data.Stat[(int)Type] = Managers.Data.Stat[(int)Type] + 1;
        Managers.Gold.UseGold(price);

        UnlockInit();
    }
}