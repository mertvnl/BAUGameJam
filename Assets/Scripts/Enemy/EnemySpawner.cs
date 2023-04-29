using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    public bool IsSpawning { get; private set; }

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private SpriteRenderer spawnMarker;
    
    private const float SPAWN_DURATION = 1.25f; //TODO: Make it generic
    private const int MARKER_TWEEN_LOOP_COUNT = 4;
    private const float MARKET_TWEEN_LOOP_DURATION = SPAWN_DURATION / MARKER_TWEEN_LOOP_COUNT;

    private int _marketTweenID;

    public void StartSpawn() 
    {
        if (IsSpawning)
            return;

        IsSpawning = true;
        MarkerTween(CompleteSpawn);
    }

    private void Awake()
    {
        _marketTweenID = GetInstanceID();
        SetMarketAlpha(0);
    }

    private void Start()
    {
        EnemySpawnerManager.Instance.AddSpawner(this);
    }

    private void CompleteSpawn() 
    {
        IsSpawning = false;
        SetMarketAlpha(0);

        Enemy enemy = Instantiate(enemyData.EnemyPrefab, transform.position, transform.rotation).GetComponent<Enemy>();
        enemy.Initialize(enemyData);
    }

    private void MarkerTween(Action onComplete = null) 
    {
        SetMarketAlpha(1);
        DOTween.Kill(_marketTweenID);
        spawnMarker.DOFade(0, MARKET_TWEEN_LOOP_DURATION).SetLoops(MARKER_TWEEN_LOOP_COUNT, LoopType.Yoyo).OnComplete(() => onComplete?.Invoke());
    }

    private void SetMarketAlpha(float alpha) 
    {
        Color color = spawnMarker.color;
        color.a = alpha;
        spawnMarker.color = color;
    }
}
