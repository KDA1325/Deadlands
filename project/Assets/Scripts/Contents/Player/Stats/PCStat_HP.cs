using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_HP : PCStat_Base
{
    [SerializeField]
    float _hp;
    [SerializeField]
    float _maxHp;
    public float _playerHpRecover;

    public float HP
    {
        get { return _hp; }

        set
        {
            _hp = Mathf.Min(value, MaxHP);

            _hpBar.SetHP((int)_hp, (int)MaxHP);

            if (_hp <= 0)
            {
                _hp = 0;
            }
        }
    }
    public float MaxHP { get { return _maxHp; } set { _maxHp = value; } }
    public float PlayerHpRecover { get { return _playerHpRecover; } set { _playerHpRecover = value; } }

    UI_PlayerHPBar _hpBar;
    PCStat_Shield _shield;
    PCStat_Defense _defense;
    PCStat_Dodge _dodge;
    PCStat_DamageReflect _damageReflect;

    public override void Init()
    {
        _hpBar = Managers.UI.MakeWorldSpace<UI_PlayerHPBar>(transform).GetComponent<UI_PlayerHPBar>();
        _shield = gameObject.GetOrAddComponent<PCStat_Shield>();
        _defense = gameObject.GetOrAddComponent<PCStat_Defense>();
        _dodge = gameObject.GetOrAddComponent<PCStat_Dodge>();
        _damageReflect = gameObject.GetOrAddComponent<PCStat_DamageReflect>();

        PlayerHpRecover = (int)GetOutGameStat(Define.OutGameStat.PlayerHpRecover);
        
        MaxHP = (int)GetOutGameStat(Define.OutGameStat.PlayerHp);

        HP = MaxHP;

        StartCoroutine("HpRecover");
    }

    public void GetDameged(EnemyStat _enemy)
    {
        float damage = _enemy.Att_Damage;
        damage = damage - (damage * _defense.Defense);

        if (_shield.GetShieldDamage(damage))
            return;
        
        if (_damageReflect.GetReflectWhether())
        {
            _enemy.GetDameged(_enemy.Att_Damage);
            return;
        }
        
        if (_dodge.GetDodgeWhether())
            return;

        HP -= damage;
        
        GameObject damageEffect = ObjectHandler.Spawn("GetDamege", Define.Layer.Effect);
        damageEffect.GetComponent<GetDamege>().Init(Mathf.Round(damage * 100) / 100);
        damageEffect.transform.position = transform.position;

        if (HP <= 0)
        {
            HP = 0;

            if (GetComponent<PCStat_Resurrection>().TryResurrection())
                return;

            OnDeath();
        }
    }

    public void OnDeath()
    {
        Managers.UI.ShowPopupUI<UI_Failed>();

        GetComponent<PCStat_PlaySpeed>().GamePause();
    }

    IEnumerator HpRecover()
    {
        while (true)
        {
            if (HP < MaxHP)
            {
                HP += PlayerHpRecover;
            }

            yield return new WaitForSeconds(10 / ObjectHandler.Player.GetComponent<PCStat_PlaySpeed>().GetCurrentPlaySpeed());
        }
    }
}