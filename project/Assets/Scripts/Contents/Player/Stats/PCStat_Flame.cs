using System.Collections;
using UnityEngine;
using static Define;

public class PCStat_Flame : PCStat_Base
{
    [SerializeField]
    int _flamePer;
    [SerializeField]
    float _flameDamage;

    public int FlamePer { get { return _flamePer; } set { _flamePer = value; } }
    public float FlameDamage { get { return _flameDamage; } set { _flameDamage = value; } }

    public override void Init()
    {
        _flameDamage = GetOutGameStat(Define.OutGameStat.FlameDamage);
    }

    public void TryFlame(EnemyStat enemyStat)
    {
        if (_flamePer > Random.Range(0, 100))
        {
            StartCoroutine(FlameDamageRoutine(enemyStat));
        }
    }

    private IEnumerator FlameDamageRoutine(EnemyStat enemyStat)
    {
        Transform effectTransform = enemyStat.gameObject.transform.Find("Flame");
        GameObject enemy = enemyStat.gameObject;

        if (effectTransform == null)
        {
            GameObject effect = ObjectHandler.Spawn("Flame", Define.Layer.SingleEffect);
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

        while (enemy.activeSelf == true)
        {
            enemyStat.GetDameged(_flameDamage);
            
            yield return new WaitForSeconds(1.0f);
        }

        yield break;
    }
}