using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{
    enum Texts
    {
        WaveText,
        SpeedText,
        GoldText,
        LevelText
    }

    enum GameObjects
    {
        ExpSlider,
        UI_StageAlarm
    }

    enum Images
    {
        StageImage
    }

    GameScene Scene;
    PCStat_PlaySpeed _playSpeed;

    public override void Init()
    {
        base.Init();

        Scene = GameObject.Find("@Scene").GetComponent<GameScene>();

        PCStat_Base player = ObjectHandler.Player.GetComponent<PCStat_Base>();

        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Slider levelSlider = Get<GameObject>((int)GameObjects.ExpSlider).GetComponent<Slider>();
        levelSlider.value = 0;

        ObjectHandler.Spawner.OnWaveClear -= PopupCurrentWave;
        ObjectHandler.Spawner.OnWaveClear += PopupCurrentWave;

        player.GetComponent<PCStat_Level>().OnPlayerLevelUp -= SetLevelText;
        player.GetComponent<PCStat_Level>().OnPlayerLevelUp += SetLevelText;

        _playSpeed = player.GetOrAddComponent<PCStat_PlaySpeed>();
        _playSpeed.SetPlaySpeedText(Get<Text>((int)Texts.SpeedText));

        player.GetComponent<PCStat_Level>().SetLevelSlider(levelSlider);
    }

    public void OnSettingButton()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);

        UI_Setting Setting = Managers.UI.ShowPopupUI<UI_Setting>().GetComponent<UI_Setting>();

        ObjectHandler.Player.GetComponent<PCStat_PlaySpeed>().GamePause();
    }

    public void OnGameSpeedUpButton()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);

        _playSpeed.SpeedUp();
    }

    public void OnGameSpeedDownButton()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);

        _playSpeed.SpeedDown();
    }

    void PopupCurrentWave(int wave)
    {
        Get<GameObject>((int)GameObjects.UI_StageAlarm).GetComponent<UI_StageAlarm>().StageAlarm();
    }

    void SetLevelText(int level)
    {
        Managers.UI.ChangeUIText(Get<Text>((int)Texts.LevelText), $"LV. {level + 1}");
    }
}