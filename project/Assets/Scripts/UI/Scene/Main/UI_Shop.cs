using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Scene
{
    [SerializeField]
    private Button[] btns;

    private bool isPurchasing = false;

    public void ShowReward50Gold()
    {
        Managers.Ads.SetReward50Gold();
        Managers.Ads.ShowAd();
    }

    public void InApp_200Gold()
    {
        StartPurchase(200);
    }

    public void InApp_600Gold()
    {
        StartPurchase(600);
    }

    public void InApp_1100Gold()
    {
        StartPurchase(1100);
    }

    public void InApp_2200Gold()
    {
        StartPurchase(2200);
    }

    private void StartPurchase(int goldAmount)
    {
        // 인앱 결제 시작 전 모든 IAPBtns 비활성화
        SetBtnsInteractable(false);

        if (!isPurchasing)
        {
            StartCoroutine(PurchaseCoroutine(goldAmount));

            isPurchasing = true;
        }
    }

    private IEnumerator PurchaseCoroutine(int goldAmount)
    {
        // Google/앱 스토어 결제 후 보상 지급, UI 출력
        Purchase(goldAmount);

        // 비활성화 유지 
        yield return new WaitForSeconds(0.1f);

        // 보상 지급 UI 출력 후 모든 IAPBtns 활성화
        SetBtnsInteractable(true);

        isPurchasing = false;
    }

    public void SetBtnsInteractable(bool interactable)
    {
        foreach (Button button in btns) 
        {
            button.interactable = interactable;
        }
    }

    private void Purchase(int goldAmount)
    {
        Managers.Gold.EarnGold(goldAmount);
        Managers.UI.ShowPopupUI<UI_Alarm>().SetText("보상이 지급되었습니다!");
    }
}