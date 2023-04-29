using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateMachine : MonoBehaviour
{
    private Enemy _enemy;
    public Enemy Enemy => _enemy == null ? _enemy = GetComponentInParent<Enemy>() : _enemy;

    private EnemyAnimator _enemyAnimator;
    public EnemyAnimator EnemyAnimator => _enemyAnimator == null ? _enemyAnimator = GetComponentInChildren<EnemyAnimator>() : _enemyAnimator;
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
