using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Angle_BulletController : Base_BulletController
{
    Vector3 dir;

    public override void Init()
    {
        base.Init();
    }

    public override void SetInfo(Transform _target, float _damage, float _speed)
    {
        SetInfo((_target.position - transform.position).normalized, _damage, _speed);
    }

    public void SetInfo(Vector3 _dir, float _damage, float _speed)
    {
        dir = _dir;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        Damage= _damage;
        Speed = _speed;
    }

    protected override void Move()
    {
        transform.position += dir * Time.deltaTime * Speed * 3;
    }
}
