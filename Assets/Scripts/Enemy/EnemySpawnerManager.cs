using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : Singleton<EnemySpawnerManager>
{
    public float LastSpawnTime { get; private set; }
    public List<IEnemySpawner> Spawners { get; private set; } = new();

    public const float SPAWN_DELAY = 0.2f; //TODO: Connect to level data.

    public void AddSpawner(IEnemySpawner spawner) 
    {
        if (Spawners.Contains(spawner))
            return;

        Spawners.Add(spawner);
    }

    public void RemoveSpawner(IEnemySpawner spawner) 
    {
        if (!Spawners.Contains(spawner))
            return;

        Spawners.Remove(spawner);
    }

    public void Update() 
    {
        CheckSpawners();
    }

    private void CheckSpawners() 
    {
        if (Spawners.Count == 0)
            return;

        if (Time.time < LastSpawnTime + SPAWN_DELAY)
            return;

        LastSpawnTime = Time.time;
        Spawners.Shuffle();

        IEnemySpawner spawner = Spawners[0];
        spawner.Spawn();
    }
}
