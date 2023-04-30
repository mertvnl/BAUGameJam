using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IEnemyTarget
{
    private float _currentHealth;
    public float CurrentHealth 
    {
        get 
        {
            return _currentHealth;
        } 
        set 
        {
            _currentHealth = value;
            OnHealthChanged.Invoke();
        } 
    }
    [field: SerializeField] public PlayerData PlayerData { get; private set; }
    public bool IsControlable { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public Transform T => transform;

    private const int FATAL_HEALTH = 0;

    [HideInInspector]
    public UnityEvent OnInitialized = new();
    [HideInInspector]
    public UnityEvent OnHit = new();
    [HideInInspector]
    public UnityEvent OnHealthChanged = new();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        IsAlive = true;
        IsControlable = true;
        CurrentHealth = UpgradeManager.Instance.GetUpgradeByType(UpgradeType.MaxHP).GetCurrentValue();
        EnemyTargetManager.Instance.AddEnemyTarget(this);
        OnInitialized.Invoke();
    }

    public void Die()
    {
        IsControlable = false;
        IsAlive = false;

        EnemyTargetManager.Instance.RemoveEnemyTarget(this);
        EventManager.OnLevelFailed.Invoke();
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
}
