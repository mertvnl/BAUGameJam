using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [field : SerializeField] public EnemyData EnemyData { get; private set; }
    [SerializeField] private SpriteRenderer spawnMarker;

    public void Spawn() 
    {
        //TODO: SpawnEnemy
    }

    
}
