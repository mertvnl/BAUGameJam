using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IEnemy
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody2D>() : _rigidbody;
    public Transform T => transform;
    public bool IsAlive { get; private set; }
    public EnemyData EnemyData { get; private set; }
    public IEnemyTarget LastTarget { get; private set; } = null;

    public void Initialize(EnemyData enemyData) 
    {
        IsAlive = true;
        EnemyData = enemyData;
        EnemyManager.Instance.AddEnemy(this);
    }

    public void Hit(int damage)
    {
        //TODO: Hit logic
        Die();
    }  

    public void SetTarget(IEnemyTarget target) 
    {
        LastTarget = target;
    }

    private void Die() 
    {
        IsAlive = false;
        EnemyManager.Instance.RemoveEnemy(this);
        PlayerLevelManager.Instance.IncreaseExperience(EnemyData.Experience);
        Destroy(gameObject);
    }
}
