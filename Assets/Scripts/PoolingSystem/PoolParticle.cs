using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolParticle : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        PoolingSystem.Instance.DestroyPoolObject(gameObject);
    }
}
