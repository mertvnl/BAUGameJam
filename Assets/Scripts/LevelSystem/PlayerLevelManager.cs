using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevelManager : Singleton<PlayerLevelManager>
{
    public int CurrentLevel => PlayerPrefs.GetInt(LEVEL_PREF_ID, 1);
    public float CurrentExperience { get; private set; }
    public float TargetExperience => DEFAULT_LEVEL_EXPERICENCE_REQUIREMENT + (CurrentLevel * LEVEL_INCREMENT_RATIO);

    private const string LEVEL_PREF_ID = "PlayerLevel";
    private const float LEVEL_INCREMENT_RATIO = 25f;
    private const float DEFAULT_LEVEL_EXPERICENCE_REQUIREMENT =  100f;

    [HideInInspector] public UnityEvent OnPlayerLevelUp = new();
    [HideInInspector] public UnityEvent OnPlayerGainExperience = new();

    private void Awake()
    {
        ResetLevel();
    }

    public void IncreaseExperience(float exp)
    {
        CurrentExperience += exp;

        if (CurrentExperience >= TargetExperience)
        {
            UpdateCurrentLevel();
        }

        OnPlayerGainExperience.Invoke();
    }

    private void UpdateCurrentLevel()
    {
        PlayerPrefs.SetInt(LEVEL_PREF_ID, CurrentLevel + 1);
        CurrentExperience = 0;
        OnPlayerLevelUp.Invoke();
    }

    private void ResetLevel()
    {
        PlayerPrefs.SetInt(LEVEL_PREF_ID, 1);
        CurrentExperience = 0;
    }
}
