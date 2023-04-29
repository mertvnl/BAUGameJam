using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody2D>() : _rigidbody;
    public bool IsAlive { get; private set; }
    public EnemyData EnemyData { get; private set; }    
    public Transform T => transform;

    public void Initialize(EnemyData enemyData) 
    {
        IsAlive = true;
        EnemyData = enemyData;
        EnemyManager.Instance.AddEnemy(this);
    }

    public void Hit()
    {
        //TODO: Hit logic
    }  

    private void Die() 
    {
        EnemyManager.Instance.RemoveEnemy(this);    
    }
}