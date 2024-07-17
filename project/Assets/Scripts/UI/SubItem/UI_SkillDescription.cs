using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillDescription : UI_Base
{
    enum Texts
    {
        SkillTitleText,
        SkillDescriptionText
    }

    Image _panelImage;
    ContentSizeFitter _sizeFitter;

    bool _show = false;

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));

        _panelImage = GetComponent<Image>();
        _sizeFitter = GetComponent<ContentSizeFitter>();

        _panelImage.color = new Color(0, 0, 0, 0);
        Get<Text>((int)Texts.SkillTitleText).color = new Color(0, 0, 0, 0);
        Get<Text>((int)Texts.SkillDescriptionText).color = new Color(0, 0, 0, 0);
    }

    float _alpha = 0;
    private void Update()
    {
        if (_show)
        {
            if (_alpha < 0.5)
            {
                _alpha += Time.deltaTime;
            }
        }
        else
        {
            if (_alpha > 0)
            {
                _alpha -= Time.deltaTime;
            }
        }

        _alpha = Mathf.Clamp(_alpha, 0, 0.5f);

        _panelImage.color = new Color(0, 0, 0, _alpha);
        Get<Text>((int)Texts.SkillTitleText).color = new Color(1, 1, 1, _alpha * 2);
        Get<Text>((int)Texts.SkillDescriptionText).color = new Color(1, 1, 1, _alpha * 2);
    }

    public void PopupSkillDescription(string skillTitle, string skillDescription)
    {
        Get<Text>((int)Texts.SkillTitleText).text = skillTitle;
        Get<Text>((int)Texts.SkillDescriptionText).text = skillDescription;

        _show = true;

        Invoke("CloseSkillDescription", 1.5f);

        // 렌더링 리프레시
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_sizeFitter.transform);
    }

    void CloseSkillDescription()
    {
        _show = false;
    }
}