using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [field: SerializeField] public List<UpgradeData> Upgrades { get; private set; } = new();
    [field: SerializeField] public List<UpgradeData> LevelUpgrades { get; private set; } = new();
    [field: SerializeField] public List<UpgradeData> MoneyUpgrades { get; private set; } = new();

    public UnityEvent<UpgradeData> OnStatUpgraded = new();

    public UpgradeData GetUpgradeByType(UpgradeType upgradeType)
    {
        return Upgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
    }

    public void ResetAllUpgrades()
    {
        Upgrades.ForEach(x => x.ResetValue());
    }
}
