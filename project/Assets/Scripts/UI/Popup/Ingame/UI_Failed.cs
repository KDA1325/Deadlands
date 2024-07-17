using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Failed : UI_Popup
{
    [SerializeField]
    GameObject _roulette;
    [SerializeField]
    GameObject _rouletteNiddle;
    [SerializeField]
    GameObject _randomBox;

    RouletteHandler _rouletteHandler;
    RandomBoxHandler _randomBoxHandler;
    enum Texts
    {
        StageText,
        CurrentWaveText,
        MaxWaveText,
        AcquiredGoldText
    }

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));

        _rouletteHandler = _rouletteNiddle.GetComponent<RouletteHandler>();
        _randomBoxHandler = _randomBox.GetComponent<RandomBoxHandler>();

        if (Managers.Data.Stat[(int)Define.SaveFile.MaxStage1 + Managers.Scene.CurrentStage - 1] < ObjectHandler.Spawner.CurrentWave)
        {
            Managers.Data.Stat[(int)Define.SaveFile.MaxStage1 + Managers.Scene.CurrentStage - 1] = ObjectHandler.Spawner.CurrentWave;
        }

        Get<Text>((int)Texts.StageText).text = $"스테이지 : {Managers.Scene.CurrentStage}";
        Get<Text>((int)Texts.CurrentWaveText).text = $"웨이브 : {ObjectHandler.Spawner.CurrentWave}";
        Get<Text>((int)Texts.MaxWaveText).text = $"최고 웨이브 : {Managers.Data.Stat[(int)Define.SaveFile.MaxStage1 + Managers.Scene.CurrentStage - 1]}";
        Get<Text>((int)Texts.AcquiredGoldText).text = $"획득한 골드 : {ObjectHandler.Player.GetComponent<PCStat_Gold>().GetEarnGold()}";

        // 에디터 상에서 테스트가 필요할 경우 조건 '|| Application.isEditor' 추가 후 진행
        // 안드로이드
        if (Application.platform == RuntimePlatform.Android || Application.isEditor)
        {
            _roulette.SetActive(true); 
            _rouletteNiddle.SetActive(true);
        }
        // iOS
        else if(Application.platform == RuntimePlatform.IPhonePlayer) 
        {
            _randomBox.SetActive(true);
        }
    }

    public void OnGetRewardButton()
    {
        // 안드로이드
        if (Application.platform == RuntimePlatform.Android || Application.isEditor)
        {
            _rouletteHandler.CheckReward();
        }
        // iOS
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _randomBoxHandler.CheckReward();
        }
    }

    public void OnRestartButton()
    {
        Managers.Scene.SceneRestart();
    }

    public void OnTitleButton()
    {
        Managers.Scene.LoadScene(Define.Scene.MainScene);
    }
}