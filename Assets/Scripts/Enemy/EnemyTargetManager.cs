using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetManager : Singleton<EnemyTargetManager>
{
    public List<IEnemyTarget> EnemyTargets { get; private set; } = new();

    public void AddEnemyTarget(IEnemyTarget enemyTarget) 
    {
        if (EnemyTargets.Contains(enemyTarget))
            return;

        EnemyTargets.Add(enemyTarget);
    }

    public void RemoveEnemyTarget(IEnemyTarget enemyTarget) 
    {
        if (!EnemyTargets.Contains(enemyTarget))
            return;

        EnemyTargets.Remove(enemyTarget);
    }

    public IEnemyTarget GetClosestEnemyTarget(Vector3 position, float range = -1) 
    {
        IEnemyTarget closestTarget = null;
        float minDistance = range > 0 ? range : float.MaxValue;

        if (EnemyTargets.Count == 0)
            return closestTarget;

        foreach (IEnemyTarget enemy in EnemyTargets)
        {
            float distance = Vector3.Distance(position, enemy.T.position);
            if (distance < minDistance)
            {
                distance = minDistance;
                closestTarget = enemy;
            }
        }

        return closestTarget;
    }
}
