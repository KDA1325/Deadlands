using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack_BulletController : Angle_BulletController
{
    EnemyStat _sniperStat;
    Vector3 dir;

    public override void Init()
    {
        base.Init();
        // TODO
    }

    public new void SetInfo(Vector3 _dir, float _damage, float _speed)
    {
        dir = _dir;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        Damage = _damage;
        Speed = _speed;
    }
    protected override void Move()
    {
        transform.position += dir * Time.deltaTime * Speed * 3;
    }

    public void GetEnemyStat(EnemyStat sniperStat)
    {
        _sniperStat = sniperStat;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.WorldsEdge)
        {
            ObjectHandler.Despawn(gameObject);
        }

        if (collision.gameObject.layer == (int)Define.Layer.Player)
        {
            if (collision.gameObject.activeSelf == false)
            {
                return;
            }

            collision.gameObject.GetComponent<PCStat_HP>().GetDameged(_sniperStat);
            ObjectHandler.Despawn(gameObject);
        }
    }
}