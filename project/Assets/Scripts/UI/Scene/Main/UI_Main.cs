using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    enum GameObjects
    {
        Panels,
        PlusUpgrade
    }
    protected enum Texts
    {
        StageLevelText,
        StageInfoText,
        GoldText,
        PlayerAttStatText,
        PlayerDefStatText,
        PlayerUtilStatText
    }
    enum panels
    {
        HomePanel,
        EnfrocePanel,
        ShopPanel,
    }
    enum Buttons
    {
        HomeButton,
        CardButton,
        ShopButton,
        AdButton,
    }

    panels _currentPanel = panels.HomePanel;
    RectTransform _panels;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        _panels = Get<GameObject>((int)GameObjects.Panels).GetComponent<RectTransform>();

        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(SoundClick);
        }
    }

    #region Buttons

    public void OnHomeButton()
    {
        _currentPanel = panels.HomePanel;
        ButtonScailing(_currentPanel);
    }
    public void OnCardButton()
    {
        _currentPanel = panels.EnfrocePanel;
        ButtonScailing(_currentPanel);
    }
    public void OnShopButton()
    {
        _currentPanel = panels.ShopPanel;
        ButtonScailing(_currentPanel);
    }
    public void SoundClick()
    {
        Managers.Sound.Play("Free UI Click Sound Effects Pack/SFX_UI_Button_Keyboard_Enter_Thick_1", Define.Sound.Effect);
    }
    void ButtonScailing(panels button)
    {
        switch (button)
        {
            case panels.HomePanel:
                Get<Button>((int)Buttons.HomeButton).GetComponent<RectTransform>().localScale = Vector3.one * 1;
                Get<Button>((int)Buttons.CardButton).GetComponent<RectTransform>().localScale = Vector3.one * 0.9f;
                Get<Button>((int)Buttons.ShopButton).GetComponent<RectTransform>().localScale = Vector3.one * 0.9f;
                break;
            case panels.EnfrocePanel:
                Get<Button>((int)Buttons.HomeButton).GetComponent<RectTransform>().localScale = Vector3.one * 0.9f;
                Get<Button>((int)Buttons.CardButton).GetComponent<RectTransform>().localScale = Vector3.one * 1;
                Get<Button>((int)Buttons.ShopButton).GetComponent<RectTransform>().localScale = Vector3.one * 0.9f;
                break;
            case panels.ShopPanel:
                Get<Button>((int)Buttons.HomeButton).GetComponent<RectTransform>().localScale = Vector3.one * 0.9f;
                Get<Button>((int)Buttons.CardButton).GetComponent<RectTransform>().localScale = Vector3.one * 0.9f;
                Get<Button>((int)Buttons.ShopButton).GetComponent<RectTransform>().localScale = Vector3.one * 1;
                break;
        }
    }

    #endregion

    private void Update()
    {
        Vector2 _targetPos = new Vector3((int)_currentPanel * -1200, 0, 0);
        Vector2 _pos = Vector2.Lerp(_panels.anchoredPosition, _targetPos, 10f * Time.deltaTime);
        _panels.anchoredPosition = _pos;
    }

    public void InitButton()
    {
        Managers.Data.InitSaveFile();
    }
}