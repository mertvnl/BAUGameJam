using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpgradePanel : FadePanelBase
{
    [SerializeField] private StatIndicator statIndicatorPrefab;
    [SerializeField] private Transform statIndicatorHolder;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (UpgradeData upgrade in UpgradeManager.Instance.LevelUpgrades)
        {
            StatIndicator statIndicator = Instantiate(statIndicatorPrefab, statIndicatorHolder);
            statIndicator.Initialize(upgrade);
        }
    }

    public override void ShowPanelWithFade(float duration, float delay = 0)
    {


        base.ShowPanelWithFade(duration, delay);
    }
}
