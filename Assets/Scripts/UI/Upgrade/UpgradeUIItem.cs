using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIItem : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeDescription;
    [SerializeField] private TextMeshProUGUI upgradeOffer;
    [SerializeField] private Image upgradeIcon;

    private UpgradeData _upgradeData;
    private int _offerValue;

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(Upgrade);
    }

    public void Initialize(UpgradeData upgradeData)
    {
        _upgradeData = upgradeData;
        _offerValue = Random.Range(_upgradeData.OfferMinValue, _upgradeData.OfferMaxValue);
        upgradeName.SetText(_upgradeData.UpgradeName);
        upgradeDescription.SetText(_upgradeData.UpgradeDescription);
        upgradeOffer.SetText("+" + _offerValue);
        upgradeIcon.sprite = _upgradeData.UpgradeIcon;
    }

    private void Upgrade()
    {
        _upgradeData.IncreaseValue(_offerValue);
    }
}
