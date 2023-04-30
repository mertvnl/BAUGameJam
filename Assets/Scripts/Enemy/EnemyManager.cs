using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<IEnemy> Enemies { get; private set; } = new();    

    public void AddEnemy(IEnemy enemy) 
    {
        if (Enemies.Contains(enemy))
            return;

        Enemies.Add(enemy);
    }

    public void RemoveEnemy(IEnemy enemy) 
    {
        if (!Enemies.Contains(enemy))
            return;

        Enemies.Remove(enemy);
    }
    
    public IEnemy GetClosestEnemy(Vector3 position, float range = -1) 
    {
        IEnemy closestEnemy = null;
        float minDistance = range > 0 ? range : float.MaxValue;

        if(Enemies.Count == 0)
            return closestEnemy;

        foreach (IEnemy enemy in Enemies)
        {
            float distance = Vector3.Distance(position, enemy.T.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
