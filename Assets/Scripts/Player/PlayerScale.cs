using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponent<Player>() : _player; 

    [SerializeField] private Transform body;

    private const float PUNCH_STRENGTH = 0.3f;
    private const float PUNCH_DURATION = 0.3f;

    private const float FAIL_DURATION = 0.2f;    

    private int _scaleTweenID;

    private void Awake()
    {
        _scaleTweenID = GetInstanceID();
    }

    private void OnEnable()
    {
        EventManager.OnLevelFailed.AddListener(FailTween);
        Player.OnHit.AddListener(HitTween);
    }

    private void OnDisable()
    {
        EventManager.OnLevelFailed.RemoveListener(FailTween);
        Player.OnHit.RemoveListener(HitTween);
    }

    private void HitTween()
    {
        DOTween.Complete(_scaleTweenID);
        body.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, 1).SetId(_scaleTweenID);
    }

    private void FailTween() 
    {
        DOTween.Complete(_scaleTweenID);
        body.DOScale(Vector3.zero, FAIL_DURATION).SetId(_scaleTweenID);
    }
}
