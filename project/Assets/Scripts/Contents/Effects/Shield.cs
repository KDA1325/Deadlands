using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    protected Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void ShildCreate()
    {
        _anim.gameObject.SetActive(true);
    }

    public void ShieldDestroy()
    {
        _anim.Play("ShieldDestroy");
    }

    public void EndEffect()
    {
        _anim.gameObject.SetActive(false);
    }
}