using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacingController : MonoBehaviour
{
    private Enemy _enemy;
    private Enemy Enemy => _enemy == null ? _enemy = GetComponent<Enemy>() : _enemy;

    [SerializeField] private SpriteRenderer visualSprite;

    private void Update()
    {
        CheckFacing();
    }

    private void CheckFacing()
    {
        if (Enemy.LastTarget == null)
            return;

        Vector3 direction = (Enemy.LastTarget.T.position - transform.position).normalized;
        switch (direction.x)
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
