using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailedPanel : FadePanelBase
{
    private const float FADE_DURATION = 0.25f;

    private void OnEnable()
    {
        EventManager.OnLevelFailed.AddListener(OnLevelFailed);
    }

    private void OnDisable()
    {
        EventManager.OnLevelFailed.RemoveListener(OnLevelFailed);
    }

    public void RetryButton() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EventManager.OnLevelRestarted.Invoke();
    }

    private void OnLevelFailed() 
    {
        ShowPanelWithFade(FADE_DURATION);
    }
}
