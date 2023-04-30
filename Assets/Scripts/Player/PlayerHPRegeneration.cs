using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHPRegeneration : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    public float Regeneration => UpgradeManager.Instance.GetUpgradeByType(UpgradeType.HPRegeneration).GetCurrentValue();

    private float _lastRegenerationTime;

    [HideInInspector]
    public UnityEvent OnHealthRegenerated = new();

    private void Update()
    {
        CheckRegeneration();
    }

    private void CheckRegeneration() 
    {
        if (!Player.IsAlive)
            return;

        if (Regeneration == 0)
            return;

        float delay = 10f / Regeneration;
        if (Time.time < _lastRegenerationTime + delay)
            return;

        _lastRegenerationTime = Time.time;
        Player.CurrentHealth += 1;

        OnHealthRegenerated.Invoke();
    } 
}
