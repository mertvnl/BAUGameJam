using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Animator Animator => _animator == null ? _animator = GetComponent<Animator>() : _animator;

    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private PlayerInput _playerInput;
    private PlayerInput PlayerInput => _playerInput == null ? _playerInput = GetComponentInParent<PlayerInput>() : _playerInput;

    private void Update()
    {
        if (!Player.IsAlive)
            return;

        SetSpeed();
    }

    private void SetSpeed() 
    {
        float speed = PlayerInput.InputXY.magnitude;
        Animator.SetFloat("Speed", speed);
    }
}
