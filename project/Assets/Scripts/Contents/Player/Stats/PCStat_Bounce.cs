using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Bounce : PCStat_Base
{
    [SerializeField]
    int _bouncePer;
    [SerializeField]
    float _bounceDecrease;
    [SerializeField]
    int _bounceMax;

    public int BouncePer { get { return _bouncePer; } set { _bouncePer = value; } }
    public float BounceDecrease { get { return _bounceDecrease; } set { _bounceDecrease = value; } }
    public int BounceMax { get { return _bounceMax; } set { _bounceMax = value; } }

    public override void Init()
    {
        
    }
}
