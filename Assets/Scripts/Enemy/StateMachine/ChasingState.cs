using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : EnemyStateBase
{
    private Rigidbody2D Rigidbody => StateMachine.Enemy.Rigidbody;
    private Vector3 CurrentPosition => StateMachine.transform.position;
    private float AttackRange => StateMachine.Enemy.EnemyData.AttackRange;

    public ChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override IEnumerator EnterState()
    {
        StateMachine.EnemyAnimator.Walk();

        while(IsActive) 
        {
            IEnemyTarget target = EnemyTargetManager.Instance.GetClosestEnemyTarget(StateMachine.transform.position);
            if (target == null)
            {
                StateMachine.SetState(new IdleState(StateMachine));
                yield break;
            }

            float distance = Vector3.Distance(target.T.position, CurrentPosition);
            if (distance <= AttackRange) 
            {
                StateMachine.SetState(new AttackingState(StateMachine));
                yield break;
            }

            StateMachine.Enemy.SetTarget(target);
            Movement(target);

            yield return null;
        }
    }

    private void Movement(IEnemyTarget enemyTarget) 
    {
        Vector2 direction = (enemyTarget.T.position - CurrentPosition).normalized;
        Rigidbody.MovePosition(StateMachine.Enemy.EnemyData.MovementSpeed * Time.fixedDeltaTime * direction + Rigidbody.position);
    }
}
