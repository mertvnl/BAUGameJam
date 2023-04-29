using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEnemyTarget
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }
    public bool IsControlable { get; private set; }
    public Transform T => transform;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        IsControlable = true;
        EnemyTargetManager.Instance.AddEnemyTarget(this);
    }

    public void Die()
    {

    }

    public void Hit()
    {
        
    }
}
