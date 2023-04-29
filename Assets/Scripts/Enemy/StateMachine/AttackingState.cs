using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : EnemyStateBase
{
    private Vector3 CurrentPosition => StateMachine.transform.position;
    private float AttackRange => StateMachine.Enemy.EnemyData.AttackRange;

    public AttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override IEnumerator EnterState()
    {
        while(IsActive) 
        {
            IEnemyTarget target = EnemyTargetManager.Instance.GetClosestEnemyTarget(StateMachine.transform.position);
            if (target == null)
            {
                StateMachine.SetState(new IdleState(StateMachine));
                yield break;
            }

            float distance = Vector3.Distance(target.T.position, CurrentPosition);
            if (distance > AttackRange)
            {
                StateMachine.SetState(new ChasingState(StateMachine));
                yield break;
            }


            yield return null;
        }      
    }
}
