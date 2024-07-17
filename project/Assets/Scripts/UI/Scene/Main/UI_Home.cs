using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Home : UI_Scene
{
    int _stage = 1;
    int _ClearStage;

    protected enum Texts
    {
        StageLevelText,
        StageInfoText,
        GoldText,
        PlayerAttStatText,
        PlayerDefStatText,
        PlayerUtilStatText
    }

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));

        SetStat();

        _ClearStage = (int)Managers.Data.Stat[(int)Define.SaveFile.ClearStage];
    }

    public void OnStageSellectLeft()
    {
        if (_stage <= 1)
        {
            UI_Alarm alarm = Managers.UI.ShowPopupUI<UI_Alarm>();
            alarm.SetText("첫번째 스테이지입니다");
            return;
        }

        --_stage;
        Managers.UI.ChangeUIText(Get<Text>((int)Texts.StageLevelText), $"{_stage}단계");
        Managers.UI.ChangeUIText(Get<Text>((int)Texts.StageInfoText), $"최대 웨이브 : {_stage * 50}");

        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(SetStat);
        }
    }

    public void OnStageSellectRight()
    {
        if (_stage >= 5)
        {
            UI_Alarm alarm = Managers.UI.ShowPopupUI<UI_Alarm>();
            alarm.SetText("최대 스테이지입니다");
            return;
        }
        else if (_stage > _ClearStage)
        {
            UI_Alarm alarm = Managers.UI.ShowPopupUI<UI_Alarm>();
            alarm.SetText("미해금 스테이지입니다");
            return;
        }


        ++_stage;
        Managers.UI.ChangeUIText(Get<Text>((int)Texts.StageLevelText), $"{_stage}단계");
        Managers.UI.ChangeUIText(Get<Text>((int)Texts.StageInfoText), $"최대 웨이브 : {_stage * 50}");
    }

    public void GameStartButton()
    {
        Managers.Scene.GameSceneStart(_stage);
    }

    public void SetStat()
    {
        Managers.UI.ChangeUIText(Get<Text>((int)Texts.PlayerAttStatText),
        @$"공격력 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerAttDamage))}
공격 속도 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerAttSpeed))}
공격 범위 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerAttRange))}
크리티컬 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerCriPercent))}
크리티컬 데미지 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerCriDamage))}
멀티샷 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.MultiShotPercent))}
멀티샷 개수 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.MultiShotCount))}
듀얼샷 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.DualShotPercent))}
듀얼샷 개수 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.DualShotCount))}
플레임 데미지 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.FlameDamage))}
프리즈 슬로우 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.FreezeSlowEffect))}
밤 데미지 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.BoomDamage))}");

        Managers.UI.ChangeUIText(Get<Text>((int)Texts.PlayerDefStatText),
        @$"체력 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerHp))}
회복 속도 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerHpRecover))}
흡혈 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.LifeDrainPercent))}
반사 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerReflectPercent))}
회피 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerEnvasionPercent))}
넉백 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerPushPercent))}");

        Managers.UI.ChangeUIText(Get<Text>((int)Texts.PlayerUtilStatText),
        $@"부활 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlayerResurrectionPercent))}
재화 출현 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.CoinObtainPercent))}
재화 획득량 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.CoinObtainRatio))}
경험치 획득량 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.ExpObtainRatio))}
게임 진행 최대 속도 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.PlaySpeedUp))}
재화 이자 지급 확률 : {Managers.UI.GetNumString(Managers.Data.GetDateFileStat(Define.OutGameStat.CoinInterestPercent))}");
    }
}