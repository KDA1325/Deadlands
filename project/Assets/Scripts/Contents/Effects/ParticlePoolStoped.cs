using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolStoped : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectHandler.Despawn(gameObject);
    }
}