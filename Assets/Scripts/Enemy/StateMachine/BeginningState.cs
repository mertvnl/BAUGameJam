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
        StateMachine.EnemyAnimator.Idle();
        yield return CHASING_WAIT;

        while (IsActive) 
        {
            IEnemyTarget target = EnemyTargetManager.Instance.GetClosestEnemyTarget(StateMachine.transform.position);
            yield return new WaitUntil(() => target != null);
            
            StateMachine.SetState(new ChasingState(StateMachine));
            yield return null;
        }
    }
}
