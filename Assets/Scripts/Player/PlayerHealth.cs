using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private void OnEnable()
    {
        UpgradeManager.Instance.OnStatUpgraded.AddListener(OnStateUpgraded);
    }

    private void OnDisable()
    {
        UpgradeManager.Instance.OnStatUpgraded.RemoveListener(OnStateUpgraded);
    }

    private void OnStateUpgraded(UpgradeData upgradeData) 
    {
        
    }
}
