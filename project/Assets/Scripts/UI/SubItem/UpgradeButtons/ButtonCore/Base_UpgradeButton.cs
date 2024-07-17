using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Base_UpgradeButton : UI_Base
{
    public int _level = 0;
    public string _skillName;

    [TextArea]
    public string _skillDescription;

    protected PCStat_Base _player;

    UI_SkillDescription _description;

    public override void Init()
    {
        _player = ObjectHandler.Player.GetComponent<PCStat_Base>();
        _description = FindObjectOfType<UI_SkillDescription>();

        base.Init();
    }

    public virtual void OnUpgrade()
    {
        ++_level;
        _description.PopupSkillDescription(_skillName, _skillDescription);

        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);
        
        PCCtrl_Skill pc = ObjectHandler.Player.GetComponent<PCCtrl_Skill>();

        pc.GetComponent<PCStat_PlaySpeed>().GamePlay();

        pc.GetComponent<PCCtrl_Skill>().AcquisitionButton.Add(this);

        Managers.UI.ClosePopupUI();

        gameObject.SetActive(false);
    }
}
