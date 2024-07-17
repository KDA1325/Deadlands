using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Split : PCStat_Base
{
    [SerializeField]
    int _splitBulletPer;
    [SerializeField]
    float _splitBulletDecrease;

    public int Split_BulletPer { get { return _splitBulletPer; } set { _splitBulletPer = value; } }
    public float Split_BulletDecrease { get { return _splitBulletDecrease; } set { _splitBulletDecrease = value; } }

    public override void Init()
    {
        
    }
}
