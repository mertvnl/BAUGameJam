using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpgradePanel : FadePanelBase
{
    [SerializeField] private StatIndicator statIndicatorPrefab;
    [SerializeField] private Transform statIndicatorHolder;

    [SerializeField] private UpgradeUIItem upgradeUIItem;
    [SerializeField] private Transform upgradeItemHolder;

    private List<UpgradeUIItem> createdUpgradeItems = new();

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        UpgradeManager.Instance.OnStatUpgraded.AddListener((x) => HidePanelWithFade(0.25f));
        PlayerLevelManager.Instance.OnPlayerLevelUp.AddListener(()=> ShowPanelWithFade(0.25f));
    }

    private void OnDisable()
    {
        UpgradeManager.Instance.OnStatUpgraded.RemoveListener((x) => HidePanelWithFade(0.25f));
        PlayerLevelManager.Instance.OnPlayerLevelUp.RemoveListener(() => ShowPanelWithFade(0.25f));
    }

    private void Initialize()
    {
        foreach (UpgradeData upgrade in UpgradeManager.Instance.LevelUpgrades)
        {
            StatIndicator statIndicator = Instantiate(statIndicatorPrefab, statIndicatorHolder);
            statIndicator.Initialize(upgrade);
        }
    }

    private void CreateRandomUpgrades()
    {
        foreach (var createdUpgradeItem in createdUpgradeItems)
            Destroy(createdUpgradeItem.gameObject);

        createdUpgradeItems.Clear();

        List<UpgradeData> randomUpgrades = new(UpgradeManager.Instance.LevelUpgrades);
        randomUpgrades.Shuffle();

        for (int i = 0; i < 3; i++)
        {
            UpgradeUIItem upgradeItem = Instantiate(upgradeUIItem, upgradeItemHolder);
            upgradeItem.Initialize(GetRandomUpgrade());
            createdUpgradeItems.Add(upgradeItem);
        }

        UpgradeData GetRandomUpgrade()
        {
            UpgradeData randomUpgrade = randomUpgrades[Random.Range(0, randomUpgrades.Count)];
            randomUpgrades.Remove(randomUpgrade);
            return randomUpgrade;
        }
    }

    public override void ShowPanelWithFade(float duration, float delay = 0)
    {
        CreateRandomUpgrades();
        Time.timeScale = 0;
        base.ShowPanelWithFade(duration, delay);
    }

    public override void HidePanelWithFade(float duration, float delay = 0)
    {
        Time.timeScale = 1;
        base.HidePanelWithFade(duration, delay);
    }
}
