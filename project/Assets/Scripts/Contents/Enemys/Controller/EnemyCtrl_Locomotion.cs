using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl_Locomotion : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    EnemyStat _stat;

    [SerializeField]
    bool _isPlayerEnter;
    [SerializeField]
    bool _isPushed;

    public bool _isPlayerScanned;

    public float _pushSpeed = 25;
    private float _sniperRange = 0.7f;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        _player = ObjectHandler.Player.gameObject;
        _stat = GetComponent<EnemyStat>();

        _isPlayerEnter = false;
        _isPlayerScanned = false;
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf && _stat.enemyType == Define.EnemyType.Sniper)
        {
            _isPlayerScanned = Physics2D.OverlapCircle(transform.position, _sniperRange, 1 << (int)Define.Layer.Player);

            if (!_isPlayerScanned)
            {
                transform.Translate((_player.transform.position - transform.position).normalized * Time.deltaTime * _stat.Move_Speed);
            }
            else
            {
                _stat.Move_Speed = 0;
            }
        }
        else if(gameObject.activeSelf && _stat.enemyType != Define.EnemyType.Sniper)
        {
            if (_isPushed)
            {
                Vector3 disVec = ObjectHandler.Player.transform.position - transform.position;
                Vector3 pushVec = disVec.normalized;

                if (disVec.magnitude > ObjectHandler.Player.GetComponent<PCStat_AttRange>().GetRange())
                    _isPushed = false;
                else
                    transform.position += -pushVec * Time.deltaTime * _pushSpeed;
            }
            else if (!_isPlayerEnter)
            {
                transform.Translate((_player.transform.position - transform.position).normalized * Time.deltaTime * _stat.Move_Speed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.Player && gameObject.activeSelf)
        {
            _isPlayerEnter = true;
        }
    }

    public void IsPushed()
    {
        _isPushed = true;
        _isPlayerEnter = false;
    }
}