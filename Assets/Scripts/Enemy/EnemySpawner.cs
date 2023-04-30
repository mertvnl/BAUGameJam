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
    private const float COMPLETE_DELAY = 0.2f;

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

    private void OnEnable()
    {
        EnemySpawnerManager.Instance.AddSpawner(this);
    }

    private void OnDisable()
    {
        EnemySpawnerManager.Instance.RemoveSpawner(this);
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
        DOTween.Kill(_marketTweenID);
        Sequence markerSequence = DOTween.Sequence();
        markerSequence.Append(spawnMarker.DOFade(1, MARKET_TWEEN_LOOP_DURATION).SetEase(Ease.InOutSine).SetId(_marketTweenID))
        .Append(spawnMarker.DOFade(0, MARKET_TWEEN_LOOP_DURATION).SetLoops(MARKER_TWEEN_LOOP_COUNT, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId(_marketTweenID))
        .AppendInterval(COMPLETE_DELAY)
        .OnComplete(() => onComplete?.Invoke()).SetId(_marketTweenID);
    }

    private void SetMarketAlpha(float alpha) 
    {
        Color color = spawnMarker.color;
        color.a = alpha;
        spawnMarker.color = color;
    }
}
