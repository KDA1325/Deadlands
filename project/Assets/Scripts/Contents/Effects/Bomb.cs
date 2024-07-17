using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnEnable()
    {
        if (gameObject.activeSelf == false)
        {
            ObjectHandler.Despawn(gameObject);
        }
    }
}
