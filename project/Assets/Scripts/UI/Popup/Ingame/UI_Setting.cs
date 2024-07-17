using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
    enum GameObjects
    {
        AcquisitionSkills
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        PCCtrl_Skill pc = ObjectHandler.Player.GetComponent<PCCtrl_Skill>();

        GameObject acquisitionSkills = Get<GameObject>((int)GameObjects.AcquisitionSkills);

        for (int i = 0; i < acquisitionSkills.transform.childCount; i++)
        {
            Managers.Resource.Destroy(acquisitionSkills.transform.GetChild(i).gameObject);
        }

        foreach (Base_UpgradeButton buttons in pc.GetComponent<PCCtrl_Skill>().AcquisitionButton)
        {
            GameObject duplicate = Managers.Resource.Instantiate(buttons.gameObject);

            duplicate.SetActive(true);
            duplicate.transform.SetParent(acquisitionSkills.transform, false);
            duplicate.GetComponent<Image>().raycastTarget = false;
            duplicate.transform.localScale = Vector3.one;
            duplicate.GetComponent<Button>().interactable = false;
        }
    }

    public void OnPlayButton()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);
        ObjectHandler.Player.GetComponent<PCStat_PlaySpeed>().GamePlay();
        Managers.UI.ClosePopupUI();
    }

    public void OnTitleButton()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.Scene.MainScene);
    }

    public void OnRestartButton()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);
        Managers.Scene.SceneRestart();
    }

    public void OnMuteButton()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);
        Managers.Sound.TogleBackGroundMusic();
    }
}