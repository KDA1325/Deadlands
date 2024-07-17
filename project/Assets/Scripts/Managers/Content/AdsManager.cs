using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Unity.VisualScripting;
using System;

public class AdsManager : IUnityAdsLoadListener, IUnityAdsShowListener
{
    const string _androidAdUnitId = "Rewarded_Android";
    const string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null;

    Action _reward = null;

    AdsButton[] _adsButtons;

    UI_Main _uiMain;

    public void Init()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        GameObject adsInitializer = GameObject.Find("@AdsInitializer");
        if (adsInitializer == null)
            Managers.Resource.Instantiate("@AdsInitializer");

        _adsButtons = GameObject.FindObjectsOfType<AdsButton>();
        _uiMain = GameObject.FindObjectOfType<UI_Main>();
    }

    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        for (int i = 0; i < _adsButtons.Length; ++i)
        {
            _adsButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);

        for (int i = 0; i < _adsButtons.Length; ++i)
        {
            _adsButtons[i].GetComponent<Button>().interactable = false;
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Managers.UI.ShowPopupUI<UI_Alarm>().SetText("보상이 지급되었습니다!");

            _reward.Invoke();
            _reward = null;

            LoadAd();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    #region 광고 리워드 지급 설정
    public void SetReward50Gold()
    {
        _reward += Reward_50Gold;
    }

    void Reward_50Gold()
    {
        Managers.Gold.EarnGold(50);
    }
    #endregion

    public void Clear()
    {

    }
}