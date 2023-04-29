using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningState : EnemyStateBase
{
    public const float CHASING_DELAY = 0.25f;
    public readonly WaitForSeconds CHASING_WAIT = new(CHASING_DELAY);

    public BeginningState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override IEnumerator EnterState()
    {
        yield return CHASING_WAIT;

        StateMachine.SetState(new ChasingState(StateMachine));

        yield break;
    }
}
