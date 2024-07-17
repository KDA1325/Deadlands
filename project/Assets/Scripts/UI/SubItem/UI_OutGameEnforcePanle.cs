using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OutGameEnforcePanle : UI_Base
{
    UI_Home homePanle;

    enum Texts
    {
        StatNameText,
        EnforceStatText
    }

    [SerializeField]
    Define.OutGameStat Type;
    Dictionary<int, List<float>> outGameStat;
    Dictionary<int, List<float>> saveFiles;

    public override void Init()
    {
        homePanle = GameObject.Find("HomePanel").GetComponent<UI_Home>();

        base.Init();

        Bind<Text>(typeof(Texts));
        outGameStat = Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat");

        TextInit();
    }

    void TextInit()
    {
        if (outGameStat[(int)Type][3] <= Managers.Data.Stat[(int)Type] &&
            Type != Define.OutGameStat.PlayerHp && Type != Define.OutGameStat.PlayerAttDamage)
        {
            Managers.UI.ChangeUIText(Get<Text>((int)Texts.EnforceStatText), $"Max");
        }
        else
        {
            string currentStat = Managers.UI.GetNumString(GetStat(0));
            string nextStat = Managers.UI.GetNumString(GetStat(1));
            string cost = Managers.UI.GetNumString(GetCost());
            Managers.UI.ChangeUIText(Get<Text>((int)Texts.EnforceStatText), $"{currentStat} >> {nextStat}  |  G x {cost}");
        }
    }

    public void OnUpgrade()
    {
        int _upgradeCost = GetCost();

        if (outGameStat[(int)Type][3] <= Managers.Data.Stat[(int)Type] &&
            Type != Define.OutGameStat.PlayerHp && Type != Define.OutGameStat.PlayerAttDamage)
        {
            Managers.UI.ShowPopupUI<UI_Alarm>().SetText("최대 레벨입니다.");
            return;
        }
        else if (Managers.Gold.CurrentGold < _upgradeCost)
        {
            Managers.UI.ShowPopupUI<UI_Alarm>().SetText("돈이 부족합니다");
            return;
        }

        Managers.Gold.UseGold(_upgradeCost);
        Managers.Data.Stat[(int)Type] += 1;

        TextInit();
        homePanle.SetStat();
    }

    float GetStat(int level)
    {
        float stat = outGameStat[(int)Type][0];

        for (int i = 0; i < (int)Managers.Data.Stat[(int)Type] + level; ++i)
        {
            stat += Mathf.Round(outGameStat[(int)Type][1] * 100f) / 100f;
        }

        return Mathf.Round(stat * 100) / 100;
    }

    int GetCost()
    {
        float cost = outGameStat[(int)Type][4];

        for (int i = 0; i < (int)Managers.Data.Stat[(int)Type]; ++i)
        {
            cost *= outGameStat[(int)Type][5];
        }

        return (int)cost;
    }
}