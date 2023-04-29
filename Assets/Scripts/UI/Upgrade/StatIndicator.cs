using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statName, statValue;

    private UpgradeData _upgradeData;

    private void OnEnable()
    {
        UpgradeManager.Instance.OnStatUpgraded.AddListener(UpdateTexts);
    }

    private void OnDisable()
    {
        UpgradeManager.Instance.OnStatUpgraded.RemoveListener(UpdateTexts);
    }

    public void Init(UpgradeData upgradeData)
    {
        _upgradeData = upgradeData;
        UpdateTexts(upgradeData);
    }

    private void UpdateTexts(UpgradeData upgradeData)
    {
        if (!ReferenceEquals(_upgradeData, upgradeData)) 
            return;

        statName.SetText(_upgradeData.UpgradeName);
        statValue.SetText(_upgradeData.GetCurrentValue().ToString());

        if (_upgradeData.GetCurrentValue() > _upgradeData.DefaultUpgradeValue)
        {
            statName.color = Color.green;
            statValue.color = Color.green;
        }
        else
        {
            statName.color = Color.white;
            statValue.color = Color.white;
        }
    }
}
