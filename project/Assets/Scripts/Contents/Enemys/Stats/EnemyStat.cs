using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EnemyStat : MonoBehaviour
{
    [SerializeField]
    float _hp;
    [SerializeField]
    float _maxHp;
    [SerializeField]
    float _attDamage;

    [SerializeField]
    float _moveSpeed;

    int _id;

    public Define.EnemyType enemyType;

    public float HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            _hpBar.SetHP(_hp, Max_HP);
        }
    }
    public float Max_HP
    {
        get { return _maxHp; }
        set
        {
            _maxHp = value;
            HP = _maxHp;
        }
    }

    public float Att_Damage { get { return _attDamage; } set { _attDamage = value; } }
    public float Move_Speed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    PCStat_Base _player;
    UI_EnemyHPBar _hpBar;
    EnemyCtrl_Locomotion _locomotion;

    Dictionary<string, Sprite> _enemySkin = new Dictionary<string, Sprite>();
    SpriteRenderer _sprite;

    Dictionary<int, List<float>> _enemysStat = new Dictionary<int, List<float>>();

    enum Stat
    {
        monstarHp = 0,
        monsterChangeHp,
        monsterAttack,
        monsterChangeAttack,
        monsterSpeed,
    }

    internal void LoadStat(EnemyType enemyType)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        switch (Managers.Scene.CurrentStage)
        {
            case 1:
                _id = 1;
                break;
            case 2:
                _id = 3;
                break;
            case 3:
                _id = 7;
                break;
            case 4:
                _id = 13;
                break;
            case 5:
                _id = 21;
                break;
            case 6:
                ;
                break;
        }

        _player = ObjectHandler.Player.GetComponent<PCStat_Base>();
        _hpBar = GetComponentInChildren<UI_EnemyHPBar>();
        _hpBar.Init();
        _locomotion = GetComponent<EnemyCtrl_Locomotion>();
    }

    public void SpawnInit(int wave, EnemyType enemyType)
    {
        LoadSkin(wave, enemyType);
        LoadStat(wave, enemyType);

        RectTransform hpBarTransform = _hpBar.GetComponent<RectTransform>();

        switch (enemyType)
        {
            case Define.EnemyType.A:
                hpBarTransform.position = new Vector3(-0.02f, -0.1f, 0);
                hpBarTransform.localScale = Vector2.one * 0.0015f;
                break;
            case Define.EnemyType.B:
                hpBarTransform.position = new Vector3(-0.02f, -0.05f, 0);
                hpBarTransform.localScale = Vector2.one * 0.001f;
                break;
            case Define.EnemyType.C:
                hpBarTransform.position = new Vector3(-0.02f, -0.25f, 0);
                hpBarTransform.localScale = Vector2.one * 0.002f;
                break;
            case Define.EnemyType.D:
                hpBarTransform.position = new Vector3(-0.02f, -0.35f, 0);
                hpBarTransform.localScale = Vector2.one * 0.003f;
                break;
            case Define.EnemyType.Thief:
                hpBarTransform.position = new Vector3(-0.02f, -0.1f, 0);
                hpBarTransform.localScale = Vector2.one * 0.0015f;
                break;
            case Define.EnemyType.Sniper:
                hpBarTransform.position = new Vector3(-0.02f, -0.05f, 0);
                hpBarTransform.localScale = Vector2.one * 0.001f;
                break;
        }

        Transform freezeEffectTransform = transform.Find("Freeze");
        if (freezeEffectTransform != null)
        {
            freezeEffectTransform.gameObject.SetActive(false);
        }

        Transform flameEffectTransform = transform.Find("Flame");
        if (flameEffectTransform != null)
        {
            flameEffectTransform.gameObject.SetActive(false);
        }

        _hpBar.gameObject.SetActive(false);
    }

    void LoadSkin(int wave, EnemyType enemyType)
    {
        if (_sprite == null)
            _sprite = gameObject.GetComponent<SpriteRenderer>();

        string skinPath = $"Art/in_game/enermy/{Managers.Scene.CurrentStage}Stage/{wave / 25 + 1}_wave_enermy/{Enum.GetName(typeof(Define.EnemyType), enemyType)}";

        if (_enemySkin.ContainsKey(skinPath))
        {
            _sprite.sprite = _enemySkin[skinPath];
        }
        else
        {
            _sprite.sprite = Managers.Resource.Load<Sprite>(skinPath);
            _enemySkin.Add(skinPath, _sprite.sprite);
        }
    }

    public void LoadStat(int wave, EnemyType enemyType)
    {
        if (_enemysStat.Count == 0)
            _enemysStat = Managers.Data.GetDataFile("MonsterFile/MonsterFile");

        int _statId = ((wave / 25 + _id) * 1000) + (int)enemyType;

        float hp = _enemysStat[_statId][(int)Stat.monstarHp];
        for (int i = 0; i < wave / 5; ++i)
        {
            hp += _enemysStat[_statId][(int)Stat.monsterChangeHp];
        }
        Max_HP = hp;

        float damage = _enemysStat[_statId][(int)Stat.monsterAttack];
        for (int i = 0; i < wave / 5; ++i)
        {
            damage += _enemysStat[_statId][(int)Stat.monsterChangeAttack];
        }
        Att_Damage = damage;

        Move_Speed = _enemysStat[_statId][(int)Stat.monsterSpeed];

        enemyType = (EnemyType)(_statId % 1000);
        this.enemyType = enemyType;
    }

    public void GetDameged(float _damage)
    {
        _hpBar.gameObject.SetActive(true);

        GameObject damageEffect = ObjectHandler.Spawn("GetDamege", Define.Layer.Effect);
        damageEffect.GetComponent<GetDamege>().Init(Mathf.Round(_damage * 100) / 100);
        damageEffect.transform.position = transform.position;

        HP -= Mathf.Max(0, _damage);

        Transform freezeEffectTransform = transform.Find("Freeze");
        if (freezeEffectTransform == null)
        {
            _player.GetComponent<PCStat_Freeze>().TryFreeze(this);
        }
        else
        {
            GameObject freezeEffect = freezeEffectTransform.gameObject;

            if (freezeEffect.activeSelf == false)
            {
                _player.GetComponent<PCStat_Freeze>().TryFreeze(this);
            }
        }

        Transform flameEffectTransform = transform.Find("Flame");
        if (flameEffectTransform == null)
        {
            _player.GetComponent<PCStat_Flame>().TryFlame(this);
        }
        else
        {
            GameObject flameEffect = flameEffectTransform.gameObject;

            if (flameEffect.activeSelf == false)
            {
                _player.GetComponent<PCStat_Flame>().TryFlame(this);
            }
        }

        if (HP <= 0)
        {
            OnDeath();
        }

        // TODO
        _player.GetComponent<PCStat_PlayerPush>().TryPush(_locomotion);
    }

    public void OnDeath()
    {
        if (_player != null && gameObject.activeSelf)
        {
            ObjectHandler.Spawn("enermy_die_particle", Define.Layer.Effect).transform.position = transform.position;

            _player.GetComponent<PCStat_Gold>().KillCompensation();
            _player.GetComponent<PCStat_Level>().KillCompensation();
            _player.GetComponent<PCStat_LifeDrain>().TryLifeDrain();
            _player.GetComponent<PCStat_Bomb>().TryBomb(this);

            foreach (Transform childTransform in transform)
            {
                GameObject child = childTransform.gameObject;

                if (child.layer == (int)Define.Layer.SingleEffect)
                {
                    ObjectHandler.Despawn(child);
                }
            }
            ObjectHandler.Despawn(gameObject);
        }
        StopAllCoroutines();
    }

    public void ChangeSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        _hpBar.transform.position = Vector3.down * 0.3f;
    }
}