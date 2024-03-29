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
    private Coroutine drillRoutine;

    private void Awake()
    {
        _scaleTweenID = GetInstanceID();
        CurrentHealth = MaxHealth;
        UpdateHealthBar();        
    }

    private void OnEnable()
    {
        EnemyTargetManager.Instance.AddEnemyTarget(this);
        drillRoutine = StartCoroutine(DrillMoneyGain()); 
    }

    private IEnumerator DrillMoneyGain()
    {
        while (IsAlive)
        {
            FloatingText fText = PoolingSystem.Instance.InstantiateFromPool("FloatingText", transform.position + Vector3.up, Quaternion.identity).GetComponent<FloatingText>();
            fText.Initialize("+$" + Random.Range(5,25), Color.green);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDisable()
    {
        EnemyTargetManager.Instance.RemoveEnemyTarget(this);
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
            if (drillRoutine != null)
                StopCoroutine(drillRoutine);
            FailTween();
            EnemyTargetManager.Instance.RemoveEnemyTarget(this);
            GameManager.Instance.TriggerFail();
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
