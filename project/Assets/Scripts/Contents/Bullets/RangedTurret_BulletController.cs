using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTurret_BulletController : Angle_BulletController
{
    Vector3 dir;

    public override void Init()
    {
        base.Init();
    }

    public new void SetInfo(Vector3 _dir, float _damage, float _speed)
    {
        dir = _dir.normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        Damage = _damage;
        Speed = _speed;
    }

    protected override void Move()
    {
        Debug.DrawLine(transform.position, transform.position + dir.normalized * Time.deltaTime * Speed, Color.red, 2f);
        transform.position += dir.normalized * Time.deltaTime * Speed * 3;
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
            
            collision.gameObject.GetComponent<EnemyStat>().GetDameged(Damage);
            ObjectHandler.Despawn(gameObject);
        }
    }
}