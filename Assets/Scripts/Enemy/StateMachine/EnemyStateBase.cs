using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase
{
    public EnemyStateMachine StateMachine { get; protected set; }
    public bool IsActive => StateMachine.CurrentState == this;

    public EnemyStateBase(EnemyStateMachine stateMachine) 
    {
        StateMachine = stateMachine;
    }

    public virtual IEnumerator EnterState() 
    {
        yield break;
    }
}
