using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCCtrl_Shot : MonoBehaviour
{
    public void Awake()
    {
        Init();
    }

    public Collider2D[] _enemys;

    PCStat_AttRange _range;
    PCStat_DualShot _dualShot;
    PCStat_MultipleTarget _multipleTarget;
    PCStat_Damage _damage;
    PCStat_AttSpeed _attSpeed;

    public void Init()
    {
        _range = GetComponent<PCStat_AttRange>();
        _dualShot = GetComponent<PCStat_DualShot>();
        _multipleTarget = GetComponent<PCStat_MultipleTarget>();
        _damage = GetComponent<PCStat_Damage>();
        _attSpeed = GetComponent<PCStat_AttSpeed>();

        StartCoroutine("Scan");
        StartCoroutine("Attack");
    }

    IEnumerator Scan()
    {
        while (true)
        {
            // Scan
            _enemys = Physics2D.OverlapCircleAll(transform.position, _range.GetRange(), 1 << (int)Define.Layer.Enemy);

            if (_enemys.Length <= 0)
                yield return null;

            // Sort
            for (int i = 0; i < _enemys.Length; i++)
            {
                for (int j = 0; j < _enemys.Length - 1; j++)
                {
                    if ((transform.position - _enemys[j].transform.position).magnitude > (transform.position - _enemys[j + 1].transform.position).magnitude)
                    {
                        Collider2D temp = _enemys[j];
                        _enemys[j] = _enemys[j + 1];
                        _enemys[j + 1] = temp;
                    }
                }
            }

            yield return null;
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitUntil(() => _enemys.Length > 0);

            int multipleTarget = _multipleTarget.GetMultipleTargetCount();

            for (int i = 0; i < Mathf.Min(_enemys.Length, multipleTarget + 1); ++i)
            {
                StartCoroutine("Shot", i);
            }

            yield return new WaitForSeconds(1 / _attSpeed.Att_Speed);
        }
    }

    IEnumerator Shot(int idx)
    {
        int shotCount = _dualShot.GetDualShotCount();
        Vector3 enemyPos = _enemys[idx].transform.position;
        

        for (int i = 0; i < shotCount; ++i)
        {
            CreateBullet(enemyPos);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void CreateBullet(Vector3 dir)
    {
        Managers.Sound.Play("ShootingSound/crossbow", Define.Sound.Effect);

        Penetration_BulltController bullet = ObjectHandler.Spawn("Penetration_Bullet", Define.Layer.Bullet).GetComponent<Penetration_BulltController>();
        
        bullet.SetInfo(dir, _damage.GetDamage(), _attSpeed.Att_Speed);
    }
}
