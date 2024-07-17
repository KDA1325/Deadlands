using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Target_BulletController : Base_BulletController
{
    [SerializeField]
    protected Transform Target;

    public override void Init()
    {
        base.Init();
    }

    public override void SetInfo(Transform _target, float _damage, float _speed)
    {
        Target = _target;
        Damage = _damage;
        Speed = _speed;
    }

    public void SetInfo(int _index, float _damage, float _speed)
    {
        var enemy = Scan();

        if (enemy.Length < _index)
            return;

        SetInfo(enemy[_index].transform, _damage, _speed);
    }

    protected override void Move()
    {
        if (Target == null || !Target.gameObject.activeSelf)
        {
            ObjectHandler.Despawn(gameObject);
            return;
        }

        transform.position += (Target.position - transform.position).normalized * Time.deltaTime * Speed * 3;

        Vector2 dir = transform.position - Target.position;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(targetAngle + 180, Vector3.forward);
    }

    protected Collider2D[] Scan()
    {
        Collider2D[] Enemys = Physics2D.OverlapCircleAll(transform.position, 100, 1 << (int)Define.Layer.Enemy);

        if (Enemys.Length <= 0)
            return null;

        for (int i = 0; i < Enemys.Length; i++)
        {
            for (int j = 0; j < Enemys.Length - 1; j++)
            {
                // TODO : If creat GameManager then replace approach throught the Gamemanager
                if ((transform.position - Enemys[j].transform.position).magnitude > (transform.position - Enemys[j + 1].transform.position).magnitude)
                {
                    Collider2D Temp = Enemys[j];
                    Enemys[j] = Enemys[j + 1];
                    Enemys[j + 1] = Temp;
                }
            }
        }

        return Enemys;
    }
}