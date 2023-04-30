using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    public Player Player => _player ??= GetComponent<Player>();

    private PlayerInput _input;
    public PlayerInput Input => _input ??= GetComponent<PlayerInput>();

    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody ??= GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (!Player.IsControlable)
            return;

        Vector2 direction = Input.InputXY.normalized;
        Rigidbody.MovePosition(GetMovementSpeed() * Time.fixedDeltaTime * direction + Rigidbody.position);
    }

    private float GetMovementSpeed() 
    {
        float speedBonus = Player.PlayerData.DefaultSpeed * UpgradeManager.Instance.GetUpgradeByType(UpgradeType.MovementSpeed).GetCurrentValue() / 100f;
        float movementSpeed = Player.PlayerData.DefaultSpeed + speedBonus;
        return movementSpeed;
    }
}
