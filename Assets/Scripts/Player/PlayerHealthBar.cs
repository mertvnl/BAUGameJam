using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponent<Player>() : _player;
    private int MaxHealth => UpgradeManager.Instance.GetUpgradeByType(UpgradeType.MaxHP).GetCurrentValue();

    [SerializeField] private Image fillImage;

    private void OnEnable()
    {
        Player.OnInitialized.AddListener(UpdateHealthBar);
        Player.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDisable()
    {
        Player.OnInitialized.RemoveListener(UpdateHealthBar);
        Player.OnHealthChanged.RemoveListener(UpdateHealthBar);
    }

    private void UpdateHealthBar() 
    {
        float fillAmount = Utilities.Remap(Player.CurrentHealth, 0, MaxHealth, 0, 1);
        fillImage.fillAmount = fillAmount;
    }
}
