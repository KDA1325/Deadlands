using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration_BulltController : Angle_BulletController
{
    int _penetrate;
    float _decrease;

    public override void Init()
    {
        base.Init();

        // TODO
        _penetrate = playerStat.GetComponent<PCStat_Penetration>().Penetration_Per;
        _decrease = playerStat.GetComponent<PCStat_Penetration>().Penetration_Decrease;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.WorldsEdge)
        {
            ObjectHandler.Despawn(gameObject);
        }

        if (collision.gameObject.layer == (int)Define.Layer.Enemy)
        {
            if (collision.gameObject.activeSelf == false)
            {
                return;
            }

            int target = 1;
            if (playerStat.GetComponent<PCStat_Bounce>().BouncePer > Random.Range(0, 100))
            {
                Bounce_BulletController bullet = ObjectHandler.Spawn("Bounce_Bullet", Define.Layer.Bullet).GetComponent<Bounce_BulletController>();
                bullet.SetInfo(target++, Damage - Damage * playerStat.GetComponent<PCStat_Bounce>().BounceDecrease, Speed);
                bullet.transform.position = transform.position;
            }
            if (playerStat.GetComponent<PCStat_Split>().Split_BulletPer > Random.Range(0, 100))
            {
                for (int i = target; i <= target + 1; ++i)
                {
                    Split_BulletContrller bullet = ObjectHandler.Spawn("Split_Bullet", Define.Layer.Bullet).GetComponent<Split_BulletContrller>();
                    bullet.SetInfo(i, Damage - Damage * playerStat.GetComponent<PCStat_Bounce>().BounceDecrease, Speed);
                    bullet.transform.position = transform.position;
                }
            }

            collision.gameObject.GetComponent<EnemyStat>().GetDameged(Damage);

            if (_penetrate <= Random.Range(0, 100))
            {
                ObjectHandler.Despawn(gameObject);
            }
            else
            {
                Damage -= Damage * _decrease;
            }
        }
    }
}
