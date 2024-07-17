using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_BulletController : MonoBehaviour
{
    [SerializeField]
    protected float Damage;

    [SerializeField]
    float _speed = 1;

    protected float Speed
    {
        get { return _speed; }
        set 
        { 
            _speed = Mathf.Min(value, 7);
        }
    }

    protected PCStat_Base playerStat;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Init()
    {
        playerStat = ObjectHandler.Player.GetComponent<PCStat_Base>();
    }

    public abstract void SetInfo(Transform _target, float _damage, float _speed);

    protected abstract void Move();

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf != false)
        {
            ObjectHandler.Despawn(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.WorldsEdge)
        {
            ObjectHandler.Despawn(gameObject);
        }
    }
}