using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyBase _enemy;
    public EnemyBase Enemy => _enemy == null ? _enemy = GetComponentInParent<EnemyBase>() : _enemy;

    public EnemyStateBase CurrentState { get; private set; }

    public void SetState(EnemyStateBase enemyState)
    {
        CurrentState = enemyState;
        StartCoroutine(CurrentState.EnterState());
    }

    private void Start()
    {
        SetState(new BeginningState(this));
    }
}
