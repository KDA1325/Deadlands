using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCCtrl_Skill : MonoBehaviour
{
    GameObject _upgradeButtons;
    Stack<Base_UpgradeButton> _buttonsStack = new Stack<Base_UpgradeButton>();

    public HashSet<Base_UpgradeButton> AcquisitionButton = new HashSet<Base_UpgradeButton>();

    public void Awake()

    {
        _upgradeButtons = Util.FindChild(gameObject, "Root_UpgradeButtons");

        GetComponent<PCStat_Level>().OnPlayerLevelUp -= PopupEnforceUI;
        GetComponent<PCStat_Level>().OnPlayerLevelUp += PopupEnforceUI;
    }

    // Skill
    void PopupEnforceUI(int level)
    {
        GetComponent<PCStat_PlaySpeed>().GamePause();
        UI_IngameEnforce enforce = Managers.UI.ShowPopupUI<UI_IngameEnforce>();
    }

    public void SetRandomUpgradeButton(Transform parent)
    {
        if (_upgradeButtons.transform.childCount <= 0)
            return;

        Base_UpgradeButton _button = GetRandomUpgradeButton();

        _buttonsStack.Push(_button);
        _button.transform.SetParent(parent);
        _button.gameObject.SetActive(true);
        _button.transform.localScale = Vector3.one * 1.4f;
    }

    public Base_UpgradeButton GetRandomUpgradeButton()
    {
        Base_UpgradeButton _button = null;

        while (true)
        {
            _button = _upgradeButtons.transform.GetChild(UnityEngine.Random.Range(0, _upgradeButtons.transform.childCount))
            .GetComponent<Base_UpgradeButton>();
            Max_UpgradeButton max = _button.GetComponent<Max_UpgradeButton>();
            if (max == null)
            {
                return _button;
            }
            else if (max.isMax == false)
            {
                return _button;
            }
        }
    }

    public void ReturnUpgradeButton()
    {
        Base_UpgradeButton _button = _buttonsStack.Pop();
        _button.transform.SetParent(_upgradeButtons.transform);
        _button.gameObject.SetActive(false);
    }
}
