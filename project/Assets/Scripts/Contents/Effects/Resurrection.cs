using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrection : MonoBehaviour
{
    public void OnAnimFinish()
    {
        ObjectHandler.Despawn(gameObject);
    }
}
