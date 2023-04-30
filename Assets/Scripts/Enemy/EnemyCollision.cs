using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Enemy _player;
    private Enemy Enemy => _player == null ? _player = GetComponentInParent<Enemy>() : _player;   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponentInParent<Player>();
        if (player != null)
        {
            player.Hit(Enemy.EnemyData.Damage);
        }
    }
}
