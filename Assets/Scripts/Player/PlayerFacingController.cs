using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacingController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput PlayerInput => _playerInput == null ? _playerInput = GetComponent<PlayerInput>() : _playerInput;

    [SerializeField] private SpriteRenderer visualSprite;

    private void Update()
    {
        CheckFacing();
    }

    private void CheckFacing()
    {
        float direction = PlayerInput.InputXY.x;
        switch (direction)
        {
            case > 0f:
                visualSprite.flipX = false;
                break;

            case < 0f:
                visualSprite.flipX = true;
                break;
        }
    }
}
