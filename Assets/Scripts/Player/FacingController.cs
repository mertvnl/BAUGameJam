using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer visualSprite;

    private Vector2 _lastPosition;

    private void Update()
    {
        HandleFacing();
    }

    private void HandleFacing()
    {
        switch (CalculateVelocity().x)
        {
            case > 0f:
                visualSprite.flipX = false;
                break;
            case < 0f:
                visualSprite.flipX = true;
                break;
        }
    }

    private Vector2 CalculateVelocity()
    {
        Vector2 currentPos = transform.position;
        Vector2 deltaPos = currentPos - _lastPosition;
        _lastPosition = currentPos;
        return deltaPos;
    }
}
