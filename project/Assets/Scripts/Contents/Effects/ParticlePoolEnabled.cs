using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticlePoolEnabled : MonoBehaviour
{
    private void OnEnable()
    {
        if (gameObject.activeSelf == false)
        {
            ObjectHandler.Despawn(gameObject);
        }
    }
}