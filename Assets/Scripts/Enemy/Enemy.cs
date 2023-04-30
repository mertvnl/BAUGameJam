using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IEnemy
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody2D>() : _rigidbody;
    public Transform T => transform;
    public float CurrentHealth { get; private set; }
    public bool IsAlive { get; private set; }
    public EnemyData EnemyData { get; private set; }
    public IEnemyTarget LastTarget { get; private set; } = null;

    private const int FATAL_HEALTH = 0;

    [HideInInspector]
    public UnityEvent OnHit = new();
    [HideInInspector]
    public UnityEvent OnDie = new();

    public void Initialize(EnemyData enemyData) 
    {
        IsAlive = true;
        EnemyData = enemyData;
        CurrentHealth = EnemyData.MaxHealth;
        EnemyManager.Instance.AddEnemy(this);
    }

    public void Hit(float damage)
    {
        if (!IsAlive)
            return;

        CurrentHealth -= damage;
        if (CurrentHealth <= FATAL_HEALTH)
            Die();

        OnHit.Invoke();
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
        OnDie.Invoke();
    }
}
