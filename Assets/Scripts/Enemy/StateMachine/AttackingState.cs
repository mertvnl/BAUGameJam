using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : EnemyStateBase
{
    public AttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override IEnumerator EnterState()
    {
        yield break;
    }
}
