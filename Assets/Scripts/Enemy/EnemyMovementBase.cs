using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementBase : MonoBehaviour
{
    private IEnemy _enemy;
    private IEnemy Enemy => _enemy == null ? _enemy = GetComponentInParent<IEnemy>() : _enemy;

    protected virtual void Update() 
    {
        Movement();
    }

    protected virtual void Movement() 
    {
        if (!Enemy.IsAlive)
            return;


    }
}
