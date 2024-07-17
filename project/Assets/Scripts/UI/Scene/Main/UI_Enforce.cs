using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Enforce : UI_Scene
{
    enum Canvases
    {
        UI_AttackCard,  
        UI_DeffCard,
        UI_UtilCard
    }

    public override void Init()
    {
        base.Init();

        Bind<Canvas>(typeof(Canvases));
    }

    public void OnAttackCardButton()
    {
        Get<Canvas>((int)Canvases.UI_AttackCard).sortingOrder = 12;
        Get<Canvas>((int)Canvases.UI_DeffCard).sortingOrder = 11;
        Get<Canvas>((int)Canvases.UI_UtilCard).sortingOrder = 11;
    }
    public void OnDeffCardButton()
    {
        Get<Canvas>((int)Canvases.UI_AttackCard).sortingOrder = 11;
        Get<Canvas>((int)Canvases.UI_DeffCard).sortingOrder = 12;
        Get<Canvas>((int)Canvases.UI_UtilCard).sortingOrder = 11;
    }
    public void OnUtilCardButton()
    {
        Get<Canvas>((int)Canvases.UI_AttackCard).sortingOrder = 11;
        Get<Canvas>((int)Canvases.UI_DeffCard).sortingOrder = 11;
        Get<Canvas>((int)Canvases.UI_UtilCard).sortingOrder = 12;
    }
}
