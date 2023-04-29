using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/Create Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string UpgradeName;
    public UpgradeType UpgradeType;
    public int DefaultUpgradeValue;
    public int OfferMaxValue;
    public int OfferMinValue;
    public Sprite UpgradeIcon;
    [TextArea]
    public string UpgradeDescription;

    public int GetCurrentValue()
    {
        return PlayerPrefs.GetInt(UpgradeType.ToString(), DefaultUpgradeValue);
    }

    public void IncreaseValue(int valueToAdd)
    {
        PlayerPrefs.SetInt(UpgradeType.ToString(), GetCurrentValue() + valueToAdd);
        UpgradeManager.Instance.OnStatUpgraded.Invoke(this);
    }

    public void ResetValue()
    {
        PlayerPrefs.SetInt(UpgradeType.ToString(), DefaultUpgradeValue);
    }
}
