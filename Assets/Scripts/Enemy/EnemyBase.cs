using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public bool IsAlive { get; private set; }
    public EnemyData EnemyData { get; private set; }    
    public Transform T => transform;

    public virtual void Initialize(EnemyData enemyData) 
    {
        IsAlive = true;
        EnemyData = enemyData;
        EnemyManager.Instance.AddEnemy(this);
    }

    public virtual void Hit()
    {
        //TODO: Hit logic
    }  

    protected virtual void Die() 
    {
        EnemyManager.Instance.RemoveEnemy(this);    
    }
}
