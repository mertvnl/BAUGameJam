using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScale : MonoBehaviour
{
    private Enemy _enemy;
    private Enemy Enemy => _enemy == null ? _enemy = GetComponent<Enemy>() : _enemy;

    [SerializeField] private Transform body;

    private const float PUNCH_STRENGTH = 0.2f;
    private const float PUNCH_DURATION = 0.3f;

    private const float FAIL_DURATION = 0.2f;

    private int _scaleTweenID;

    private void Awake()
    {
        _scaleTweenID = GetInstanceID();
    }

    private void OnEnable()
    {
        Enemy.OnDie.AddListener(DieTween);
        Enemy.OnHit.AddListener(HitTween);
    }

    private void OnDisable()
    {
        Enemy.OnDie.RemoveListener(DieTween);
        Enemy.OnHit.RemoveListener(HitTween);
    }

    private void HitTween()
    {
        DOTween.Complete(_scaleTweenID);
        body.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, 1).SetId(_scaleTweenID);
    }

    private void DieTween()
    {
        DOTween.Complete(_scaleTweenID);
        body.DOScale(Vector3.zero, FAIL_DURATION).SetId(_scaleTweenID).OnComplete(() => Destroy(gameObject));
    }
}
