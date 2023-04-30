using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsLevelFailed { get; private set; }

    private void OnEnable()
    {
        EventManager.OnLevelRestarted.AddListener(() => IsLevelFailed = false);
    }

    private void OnDisable()
    {
        EventManager.OnLevelRestarted.RemoveListener(() => IsLevelFailed = false);
    }

    public void TriggerFail() 
    {
        if (IsLevelFailed)
            return;

        IsLevelFailed = true;
        EventManager.OnLevelFailed.Invoke();
    }
}
