using System.Collections;
using UnityEngine;

public class PCStat_Bomb : PCStat_Base
{
    [SerializeField]
    int _bombPer;
    [SerializeField]
    float _bombRange;
    [SerializeField]
    float _bombDamage;

    public int BombPer { get { return _bombPer; } set { _bombPer = value; } }
    public float BombRange { get { return _bombRange; } set { _bombRange = value; } }
    public float BombDamage { get { return _bombDamage; } set { _bombDamage = value; } }

    public override void Init()
    {
        BombDamage = GetOutGameStat(Define.OutGameStat.BoomDamage);
    }

    public void TryBomb(EnemyStat enemyStat)
    {
        if (_bombPer > Random.Range(0, 100))
        {
            StartCoroutine(bombDamage(_bombRange, _bombDamage, enemyStat));
        }
    }

    private IEnumerator bombDamage(float _bombRange, float _bombDamage, EnemyStat enemyStat)
    {
        GameObject effect = ObjectHandler.Spawn("Bomb", Define.Layer.Effect);
        effect.transform.position = enemyStat.transform.position;
        effect.transform.localScale = new Vector3(_bombRange, _bombRange, _bombRange);

        Collider2D[] Enemys = Physics2D.OverlapCircleAll(enemyStat.gameObject.transform.position, _bombRange, 1 << (int)Define.Layer.Enemy);

        foreach (Collider2D enemy in Enemys)
        {
            yield return null;

            enemy.GetComponent<EnemyStat>().GetDameged(_bombDamage);
        }

        yield return new WaitForSeconds(0.3f);
        
        ObjectHandler.Despawn(effect);
        
    }
}