using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Att_Range : UI_Base
{
    enum GameObjects
    {
        RangeImage
    }

    RectTransform _rangeRect;

    float _attRange;

    public float AttRange
    { 
        get 
        { 
            return _attRange; 
        } 
        set
        {
            _attRange = value;

            _rangeRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _attRange);
            _rangeRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _attRange);
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        _rangeRect = Get<GameObject>((int)GameObjects.RangeImage).GetComponent<RectTransform>();
    }
}
