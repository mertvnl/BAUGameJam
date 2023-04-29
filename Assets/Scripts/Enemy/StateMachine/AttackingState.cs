using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : EnemyStateBase
{
    private Vector3 CurrentPosition => StateMachine.transform.position;
    private EnemyData EnemyData => StateMachine.Enemy.EnemyData;
    private float AttackRange => StateMachine.Enemy.EnemyData.AttackRange;
    private float _lastHitTime;

    public AttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override IEnumerator EnterState()
    {
        while(IsActive) 
        {
            IEnemyTarget target = StateMachine.Enemy.LastTarget;
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

            Attack(target);
            yield return null;
        }      
    }

    private void Attack(IEnemyTarget target) 
    {
        if (Time.time < _lastHitTime + EnemyData.AttackCooldown)
            return;

        _lastHitTime = Time.time;
        StateMachine.EnemyAnimator.Attack(() => target.Hit(EnemyData.Damage));
    }
}
