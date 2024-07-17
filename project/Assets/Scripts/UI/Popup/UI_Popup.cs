using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base // 상속
{
    public override void SetInfo(int _order)
    {
        base.SetInfo(_order);
    }

    public void ClosePopupUI()
    {
        Clear();

        Managers.UI.ClosePopupUI();
    }

    public virtual void Clear()
    {

    }
}