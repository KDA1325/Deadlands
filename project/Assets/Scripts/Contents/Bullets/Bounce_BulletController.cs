using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_BulletController : Target_BulletController
{
    int bounceCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.WorldsEdge)
        {
            ObjectHandler.Despawn(gameObject);
        }

        if (collision.gameObject.transform == Target)
        {
            if (playerStat.GetComponent<PCStat_Bounce>().BouncePer > Random.Range(0, 100) && bounceCount < playerStat.GetComponent<PCStat_Bounce>().BounceMax)
            {
                ++bounceCount;
                var enemy = Scan();

                if (enemy.Length < 1)
                    return;

                Target = enemy[1].transform;
                Damage -= Damage * playerStat.GetComponent<PCStat_Bounce>().BounceDecrease;
            }
            else
            {
                ObjectHandler.Despawn(gameObject);
            }
            
            collision.gameObject.GetComponent<EnemyStat>().GetDameged(Damage);
        }
    }
}
