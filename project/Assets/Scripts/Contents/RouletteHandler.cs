using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteHandler : MonoBehaviour
{
    public RectTransform wheel;
    public Image imageToRotate;
    public GameObject UI_Failed;
    public float rotationSpeed = 150f;

    bool _update;
    bool isReceive;

    private void Start()
    {
        _update = true;
        isReceive = false;
    }

    void Update()
    {
        if(_update)
        RotateRoulette();
    }

    void RotateRoulette()
    {
        if (UI_Failed != null && UI_Failed.activeSelf && imageToRotate != null)
        {
            float currentAngle = transform.rotation.eulerAngles.z;

            if (currentAngle >= 90f && currentAngle <= 180f)
            {
                rotationSpeed = -Mathf.Abs(rotationSpeed);
            }
            else if (currentAngle <= 270f && currentAngle >= 180f)
            {
                rotationSpeed = Mathf.Abs(rotationSpeed);
            }

            transform.Rotate(Vector3.forward * rotationSpeed * Time.unscaledDeltaTime);
        }
    }
    private void DisplayRewardUI()
    {
        UI_Alarm alarm = Managers.UI.ShowPopupUI<UI_Alarm>();
        alarm.SetText("보상이 획득 되었습니다! \n돌아가기 버튼을 눌러 주세요");
    }

    public void CheckReward()
    {
        _update = false;
        float currentAngle = wheel.rotation.eulerAngles.z;
        float earnGold = ObjectHandler.Player.GetComponent<PCStat_Gold>().GetEarnGold();

        if(isReceive != true)
        {
            if (currentAngle <= 90 && currentAngle > 66)
            {
                Managers.Gold.EarnGold((int)((earnGold * 1.4) - earnGold));
            }
            else if (currentAngle <= 66 && currentAngle > 35)
            {
                Managers.Gold.EarnGold((int)((earnGold * 1.2) - earnGold));
            }
            else if ((currentAngle <= 35 && currentAngle > 0) || currentAngle <= 360 && currentAngle > 325)
            {
                Managers.Gold.EarnGold((int)((earnGold * 1) - earnGold));
            }
            else if (currentAngle <= 325 && currentAngle > 290)
            {
                Managers.Gold.EarnGold((int)((earnGold * 2.5) - earnGold));
            }
            else if (currentAngle <= 290 && currentAngle > 270)
            {
                Managers.Gold.EarnGold((int)((earnGold * 4) - earnGold));
            }
        }

        isReceive = true;

        DisplayRewardUI();
    }
}