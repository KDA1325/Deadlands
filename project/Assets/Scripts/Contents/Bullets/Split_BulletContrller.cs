using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Split_BulletContrller : Target_BulletController
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform == Target)
        {
            collision.gameObject.GetComponent<EnemyStat>().GetDameged(Damage);
            ObjectHandler.Despawn(gameObject);
        }
    }
}
