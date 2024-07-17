using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RandomBoxHandler : MonoBehaviour
{
    //[SerializeField]
    [SerializeField]
    private GameObject _randomBox;
    private GameObject _reward;
    private Color orgColor;
    private float _rewardPer;
    private bool isReceive;


    public void Awake()
    {
        orgColor = new Color(1f, 1f, 1f, 1f);
        isReceive = false;
    }

    private IEnumerator RandomBoxFadeOut(GameObject reward)
    {
        Color tempColor = orgColor;

        _randomBox.GetComponent<Image>().color = tempColor;

        while (_randomBox.GetComponent<Image>().color.a > 0.2f)
        {
            tempColor.a -= 0.05f;
            _randomBox.GetComponent<Image>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.2f);

        while (_randomBox.GetComponent<Image>().color.a < 1f)
        {
            tempColor.a += 0.3f;
            _randomBox.GetComponent<Image>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.1f);

        while (_randomBox.GetComponent<Image>().color.a > 0f)
        {
            tempColor.a -= 0.3f;
            _randomBox.GetComponent<Image>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        tempColor.a = 0f;
        _randomBox.GetComponent<Image>().color = tempColor;

        StartCoroutine("RewardFadeIn", reward);
    }

    private IEnumerator RewardFadeIn(GameObject reward)
    {
        CanvasGroup cg = reward.GetComponent<CanvasGroup>();
        cg.alpha = 0f;

        float fadeTime = 1f;
        float elapsedTime = 0f;

        while (elapsedTime <= fadeTime)
        {
            float deltaAlpha = Time.unscaledDeltaTime / fadeTime;
            cg.alpha += deltaAlpha;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        cg.alpha = 1f;

        DisplayRewardUI();
    }

    private void DisplayRewardUI()
    {
        UI_Alarm alarm = Managers.UI.ShowPopupUI<UI_Alarm>();
        alarm.SetText("보상이 획득 되었습니다! \n돌아가기 버튼을 눌러 주세요");
    }

    public void CheckReward()
    {
        float earnGold = ObjectHandler.Player.GetComponent<PCStat_Gold>().GetEarnGold();

        if (isReceive != true)
        {
            _rewardPer = Random.Range(0, 100);

            if (_rewardPer <= 27f)
            {
                _reward = GameObject.Find("Reward_1");

                StartCoroutine("RandomBoxFadeOut", _reward);
            }
            else if (_rewardPer <= 52f) // 27% + 25% = 52
            {
                _reward = GameObject.Find("Reward_1.2");

                StartCoroutine("RandomBoxFadeOut", _reward);

                Managers.Gold.EarnGold((int)((earnGold * 1.2) - earnGold));
            }
            else if (_rewardPer <= 75f) // 52 + 23% = 75
            {
                _reward = GameObject.Find("Reward_1.4");

                StartCoroutine("RandomBoxFadeOut", _reward);

                Managers.Gold.EarnGold((int)((earnGold * 1.4) - earnGold));
            }
            else if (_rewardPer <= 90f) // 75 + 15% = 90
            {
                _reward = GameObject.Find("Reward_2.5");

                StartCoroutine("RandomBoxFadeOut", _reward);

                Managers.Gold.EarnGold((int)((earnGold * 2.5) - earnGold));
            }
            else // 10% = 90 ~ 100
            {
                _reward = GameObject.Find("Reward_4");

                StartCoroutine("RandomBoxFadeOut", _reward);

                Managers.Gold.EarnGold((int)((earnGold * 4) - earnGold));
            }
        }

        isReceive = true;

        //Invoke("DisplayRewardUI", 10f);
    }
}