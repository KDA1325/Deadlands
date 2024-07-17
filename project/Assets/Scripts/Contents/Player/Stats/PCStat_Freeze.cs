using System.Collections;
using UnityEngine;

public class PCStat_Freeze : PCStat_Base
{
    [SerializeField]
    int _freezePer;

    [SerializeField]
    float _freezeSlow;

    const float FreezeSlowDefault = 0.15f;


    public int FreezePer { get { return _freezePer; } set { _freezePer = value; } }
    public float FreezeSlow { get { return _freezeSlow; } set { _freezeSlow = value; } }

    public override void Init()
    {
        _freezeSlow = GetOutGameStat(Define.OutGameStat.FreezeSlowEffect);
    }

    public void TryFreeze(EnemyStat enemyStat)
    {
        if (_freezePer > Random.Range(0, 100))
        {
            StartCoroutine(FreezeApplyRoutine(enemyStat));
        }
    }

    private IEnumerator FreezeApplyRoutine(EnemyStat enemyStat)
    {
        Transform effectTransform = enemyStat.gameObject.transform.Find("Freeze");
        GameObject enemy = enemyStat.gameObject;

        if (effectTransform == null)
        {
            GameObject effect = ObjectHandler.Spawn("Freeze", Define.Layer.SingleEffect);
            effect.transform.SetParent(enemyStat.gameObject.transform);
            effect.transform.localPosition = Vector3.zero;
        }
        else
        {
            GameObject effect = effectTransform.gameObject;
            effect.transform.localPosition = Vector3.zero;

            effect.SetActive(true);
        }

        yield return null;

        if (enemy.activeSelf == true)
        {
            enemyStat.Move_Speed -= enemyStat.Move_Speed * (FreezeSlowDefault + _freezeSlow / 100) ;
            Debug.Log(enemyStat.Move_Speed);
        }
    }
}