using System.Collections;
using System.Linq;
using UnityEngine;

public class PCStat_RangedTurret : PCStat_Base
{
    [SerializeField]
    float _attackDamage;
    [SerializeField]
    float _attackRange;
    [SerializeField]
    float _rotationSpeed;

    float _angle;
    public bool _isTurretActive;

    PCStat_AttSpeed _attSpeed;
    PCStat_AttRange _playerAttackRange;
    GameObject _turret;
    Vector3 _dir;

    public Collider2D[] _enemys;

    public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float RotationSpeed { get { return _rotationSpeed; } set { _rotationSpeed = value; } }

    public override void Init()
    {
        _attSpeed = GetComponent<PCStat_AttSpeed>();
        _playerAttackRange = GetComponent<PCStat_AttRange>();

        _isTurretActive = false;
    }

    public void TurretSpawn()
    {
        _turret = Managers.Resource.Instantiate("EffectPrefabs/RangedTurret");

        _isTurretActive = true;

        StartCoroutine("Scan");
        StartCoroutine("Shot");
    }

    private void FixedUpdate()
    {
        if (_turret != null && _turret.activeSelf)
        {
            _angle += _rotationSpeed * Time.deltaTime;

            float x = Mathf.Cos(_angle) * _playerAttackRange.GetRange();
            float y = Mathf.Sin(_angle) * _playerAttackRange.GetRange();

            _turret.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        }
    }

    IEnumerator Scan()
    {
        while (true)
        {
            // Scan
            _enemys = Physics2D.OverlapCircleAll(_turret.transform.position, _attackRange, 1 << (int)Define.Layer.Enemy);

            if (_enemys.Length <= 0)
                yield return null;

            // Sort
            for (int i = 0; i < _enemys.Length; i++)
            {
                for (int j = 0; j < _enemys.Length - 1; j++)
                {
                    EnemyStat enemy1 = _enemys[j].GetComponent<EnemyStat>();
                    EnemyStat enemy2 = _enemys[j + 1].GetComponent<EnemyStat>();

                    if (enemy1 != null && enemy2 != null && enemy1.enemyType != Define.EnemyType.Sniper && enemy2.enemyType == Define.EnemyType.Sniper)
                    {
                        Collider2D temp = _enemys[j];
                        _enemys[j] = _enemys[j + 1];
                        _enemys[j + 1] = temp;
                    }
                    else if (enemy1 != null && enemy2 != null && (_turret.transform.position - _enemys[j].transform.position).magnitude > (_turret.transform.position - _enemys[j + 1].transform.position).magnitude)
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

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitUntil(() => _enemys.Length > 0);

            for (int i = 0; i < _enemys.Length; ++i)
            {
                if (gameObject.activeSelf && _enemys != null)
                {
                    Vector3 enemyPos = _enemys[i].transform.position;

                    CreateBullet(enemyPos);
                }

                yield return new WaitForSeconds(1 / _attSpeed.Att_Speed);
            }

            _enemys = _enemys.Where(enemy => enemy != null).ToArray();
        }
    }

    void CreateBullet(Vector3 dir)
    {
        _dir = (dir - _turret.transform.position).normalized;
        Managers.Sound.Play("shootingsound/crossbow", Define.Sound.Effect);

        RangedTurret_BulletController bullet = ObjectHandler.Spawn("RangedTurret_Bullet", Define.Layer.Bullet).GetComponent<RangedTurret_BulletController>();
        bullet.SetInfo(_dir, _attackDamage, _attSpeed.Att_Speed);
        bullet.transform.position = _turret.transform.position;
    }
}