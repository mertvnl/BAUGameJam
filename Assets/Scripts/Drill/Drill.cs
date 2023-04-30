using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour, IEnemyTarget
{
    public bool IsAlive { get; private set; } = true;

    public Transform T => transform;

    public float CurrentHealth { get; private set; }
    private int MaxHealth => UpgradeManager.Instance.GetUpgradeByType(UpgradeType.MaxHP).GetCurrentValue();

    [SerializeField] private Transform body;
    [SerializeField] private Image fillImage;

    private const float PUNCH_STRENGTH = 0.2f;
    private const float PUNCH_DURATION = 0.3f;

    private const float FAIL_DURATION = 0.2f;
    private const int FATAL_HEALTH = 0;

    private int _scaleTweenID;

    private void Awake()
    {
        _scaleTweenID = GetInstanceID();
        CurrentHealth = MaxHealth;
        UpdateHealthBar();
        EnemyTargetManager.Instance.AddEnemyTarget(this);
    }

    public void Hit(float damage)
    {
        if (!IsAlive)
            return;

        CurrentHealth -= damage;
        UpdateHealthBar();
        HitTween();

        if (CurrentHealth <= FATAL_HEALTH)
        {
            IsAlive = false;
            FailTween();
            EnemyTargetManager.Instance.RemoveEnemyTarget(this);
            EventManager.OnLevelFailed.Invoke();
        }
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

    private void UpdateHealthBar()
    {
        float fillAmount = Utilities.Remap(CurrentHealth, 0, MaxHealth, 0, 1);
        fillImage.fillAmount = fillAmount;
    }
}
