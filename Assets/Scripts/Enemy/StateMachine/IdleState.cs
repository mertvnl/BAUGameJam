using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyStateBase
{
    public IdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override IEnumerator EnterState()
    {
        while (IsActive)
        {
            IEnemyTarget target = EnemyTargetManager.Instance.GetClosestEnemyTarget(StateMachine.transform.position);
            if (target != null)
            {
                StateMachine.SetState(new ChasingState(StateMachine));
                yield break;
            }
            yield return null;
        }
    }
}


