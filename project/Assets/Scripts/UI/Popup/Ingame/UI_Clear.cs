using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clear : UI_Popup
{
    enum Texts
    {
        StageText,
        AcquiredGoldText
    }

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));

        Get<Text>((int)Texts.StageText).text = $"스테이지 : {Managers.Scene.CurrentStage}";
        Get<Text>((int)Texts.AcquiredGoldText).text = $"획득한 골드 : {ObjectHandler.Player.GetComponent<PCStat_Gold>().GetEarnGold()}";

        Managers.Data.Stat[(int)Define.SaveFile.MaxStage1 + Managers.Scene.CurrentStage - 1] = ObjectHandler.Spawner.CurrentWave;
    }

    public void OnRestartButton()
    {
        Reword();

        Managers.Scene.SceneRestart();
    }

    public void OnTitleButton()
    {
        Reword();

        Managers.Scene.LoadScene(Define.Scene.MainScene);
    }

    public void OnGoToNextButton()
    {
        Reword();

        Managers.Scene.GameSceneStart(Managers.Scene.CurrentStage + 1);
    }

    void Reword()
    {
        int goldInterestPercent = (int)Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat")[(int)Define.SaveFile.CoinInterestPercent][0];

        int interest = ObjectHandler.Spawner.CurrentWave / 10;
        for (int i = 0; i < interest; i++)
        {
            if (goldInterestPercent > Random.Range(0, 100))
            {
                int gold = (int)Mathf.Floor(Managers.Gold.CurrentGold * 1.05f);
                gold = Mathf.Min(gold, 100000);

                Managers.Gold.EarnGold(gold);
            }
        }
    }
}