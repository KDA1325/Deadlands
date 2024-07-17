using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Shield : PCStat_Base
{
    [SerializeField]
    float _shildHP;

    float _shildMaxHP;
    float _shildCraftPer;
    float _shildCraftSec;

    public float Shild_HP { get { return _shildHP; } set { _shildHP = value; } }

    public float Shild_Max_HP { get { return _shildMaxHP; } set { _shildMaxHP = value; } }
    public float Shild_Craft_Per { get { return _shildCraftPer; } set { _shildCraftPer = value; } }
    public float Shild_Craft_Sec
    {
        get { return _shildCraftSec; }
        set
        {
            _shildCraftSec = value;

            if (_shildCraftSec > 0 && _shildCraftPer > 0)
            {
                StopCoroutine("CraftShild");
                StartCoroutine("CraftShild");
            }
        }
    }

    Shield _shield = null;

    PCStat_HP _hp;

    public override void Init()
    {
        _hp = GetComponent<PCStat_HP>();
    }

    public bool GetShieldDamage(float damage)
    {
        if (Shild_HP > 0)
        {
            Shild_HP -= damage;

            if (Shild_HP <= 0)
                _shield.ShieldDestroy();

            return true;
        }

        return false;
    }

    IEnumerator CraftShild()
    {
        while (true)
        {
            if (_shield == null)
            {
                GameObject shieldEffect = Managers.Resource.Instantiate("EffectPrefabs/Shield");
                _shield = shieldEffect.GetComponent<Shield>();
                shieldEffect.transform.SetParent(transform);
            }

            _shield.ShildCreate();

            Shild_HP = _hp.HP * Shild_Craft_Per;

            yield return new WaitUntil(() => Shild_Max_HP > Shild_HP);
            yield return new WaitForSeconds(Shild_Craft_Sec);
        }
    }
}
