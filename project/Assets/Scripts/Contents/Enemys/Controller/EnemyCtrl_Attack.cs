using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyCtrl_Attack : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    EnemyStat _stat;
    PCStat_HP _playerHp;
    EnemyCtrl_Locomotion _locomotion;
    Vector3 dir;

    float _attackTime;
    bool _goldStolen = false;

    private void Update()
    {
        _attackTime += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!_goldStolen && collider.gameObject.layer == (int)Define.Layer.Player && gameObject.activeSelf)
        {
            if (_attackTime >= 1)
            {
                if (_stat != null)
                {
                    if (_stat.enemyType == Define.EnemyType.Thief)
                    {
                        Managers.Gold.UseGold((int)_stat.Att_Damage);
                        _goldStolen = true;
                    }
                    else
                    {
                        _attackTime = 0;
                        _playerHp.GetDameged(_stat);
                    }
                }
            }
        }
    }

    public IEnumerator SniperShot()
    {
        while(true)
        {
            if(gameObject.activeSelf && _locomotion._isPlayerScanned) 
            {
                Vector3 playerPos = _player.transform.position;

                CreateBullet(playerPos, _stat);

                yield return new WaitForSeconds(2);
            }

            yield return null;
        }
    }

    public void CreateBullet(Vector3 _dir, EnemyStat _sniperStat)
    {
        dir = (_dir - transform.position).normalized;
        Managers.Sound.Play("ShootingSound/crossbow", Define.Sound.Effect);

        EnemyRangeAttack_BulletController bullet = ObjectHandler.Spawn("EnemyRangeAttack_Bullet", Define.Layer.Bullet).GetComponent<EnemyRangeAttack_BulletController>();
        bullet.SetInfo(dir, _sniperStat.Att_Damage, 2);
        bullet.GetEnemyStat(_sniperStat);
        bullet.transform.position = transform.position;
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        _player = ObjectHandler.Player.gameObject;
        _playerHp = ObjectHandler.Player.GetComponent<PCStat_HP>();
        _stat = GetComponent<EnemyStat>();
        _locomotion = GetComponent<EnemyCtrl_Locomotion>();

        StartCoroutine("SniperShot");
    }
}
