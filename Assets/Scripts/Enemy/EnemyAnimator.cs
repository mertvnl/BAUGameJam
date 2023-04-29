using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private Animator Animator => _animator == null ? _animator = GetComponentInChildren<Animator>() : _animator;

    private const string IDLE = "Idle";
    private const string WALK = "Walk";
    private const string ATTACK = "Attack";

    private Action _attackCompleteAction = null;

    public void Attack(Action onAttackComplete = null) 
    {
        _attackCompleteAction = onAttackComplete;
        Animator.SetTrigger(ATTACK);
    }

    public void OnAttackComplete() 
    {
        _attackCompleteAction?.Invoke();
    }    

    public void Walk() 
    {
        Animator.SetTrigger(WALK);
    }

    public void Idle() 
    {
        Animator.SetTrigger(IDLE);
    }
}
