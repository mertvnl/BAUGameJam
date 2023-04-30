using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : Singleton<EnemySpawnerManager>
{
    public bool IsActive { get; private set; } = true;
    public float LastSpawnTime { get; private set; }
    public float CurrentSpawnDelay { get; private set; } = MAX_DELAY;
    public List<IEnemySpawner> Spawners { get; private set; } = new();

    public const float MAX_DELAY = 1f;
    public const float MIN_DELAY = 0.25f;
    public const float DECREASE_SPEED = 0.25f;

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

    private void OnEnable()
    {
        EventManager.OnLevelFailed.AddListener(() => IsActive = false);
    }

    private void OnDisable()
    {
        EventManager.OnLevelFailed.RemoveListener(() => IsActive = false);
    }

    private void Update() 
    {
        CheckSpawners();
    }

    private void CheckSpawners() 
    {
        if (!IsActive)
            return;

        if (Spawners.Count == 0)
            return;

        if (Time.time < LastSpawnTime + CurrentSpawnDelay)
            return;

        LastSpawnTime = Time.time;
        Spawners.Shuffle();

        IEnemySpawner spawner = Spawners[0];
        spawner.StartSpawn();

        CurrentSpawnDelay -= Time.deltaTime * DECREASE_SPEED;
        CurrentSpawnDelay = Mathf.Max(MIN_DELAY, CurrentSpawnDelay);
    }
}
