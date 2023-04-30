using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI levelIndicator;

    private Tween fillTween;

    private void OnEnable()
    {
        PlayerLevelManager.Instance.OnPlayerLevelUp.AddListener(UpdateVisuals);
        PlayerLevelManager.Instance.OnPlayerGainExperience.AddListener(UpdateVisuals);
    }

    private void OnDisable()
    {
        PlayerLevelManager.Instance.OnPlayerLevelUp.RemoveListener(UpdateVisuals);
        PlayerLevelManager.Instance.OnPlayerGainExperience.RemoveListener(UpdateVisuals);
    }

    private void Start()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        float targetFillValue = Utilities.Remap(PlayerLevelManager.Instance.CurrentExperience, 0, PlayerLevelManager.Instance.TargetExperience, 0, 1);

        if (fillTween != null)
            fillTween.Kill();

        fillTween = fillImage.DOFillAmount(targetFillValue, 0.1f).SetUpdate(true);
        levelIndicator.SetText("LV." + PlayerLevelManager.Instance.CurrentLevel);
    }
}
