using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageAlarm : UI_Base
{
    enum Images
    {
        StageImage
    }
    enum Texts
    {
        WaveText
    }

    Image _stageImage;
    Text _waveText;

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        _stageImage = Get<Image>((int)Images.StageImage);
        _waveText = Get<Text>((int)Texts.WaveText);

        StageAlarm();
    }

    public void StageAlarm()
    {
        StartCoroutine("FadeIn");

        Managers.UI.ChangeUIText(_waveText, $"Wave {ObjectHandler.Spawner.CurrentWave}");
    }

    IEnumerator FadeIn()
    {
        float a = 0;
        _stageImage.color = new Vector4(1, 1, 1, a);
        _waveText.color = new Vector4(0, 0, 0, a);
        while (_stageImage.color.a <= 1)
        {
            _stageImage.color = new Vector4(1, 1, 1, a);
            _waveText.color = new Vector4(0, 0, 0, a);
            a += Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        float a = 1;
        _stageImage.color = new Vector4(1, 1, 1, a);
        _waveText.color = new Vector4(0, 0, 0, a);
        while (_stageImage.color.a >= 0)
        {
            _stageImage.color = new Vector4(1, 1, 1, a);
            _waveText.color = new Vector4(0, 0, 0, a);
            a -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}