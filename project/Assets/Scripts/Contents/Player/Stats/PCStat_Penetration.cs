using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStat_Penetration : PCStat_Base
{
    [SerializeField]
    int _penetrationPer;
    [SerializeField]
    float _penetrationDecrease;

    public int Penetration_Per { get { return _penetrationPer; } set { _penetrationPer = value; } }
    public float Penetration_Decrease { get { return _penetrationDecrease; } set { _penetrationDecrease = value; } }

    public override void Init()
    {
        
    }


}
