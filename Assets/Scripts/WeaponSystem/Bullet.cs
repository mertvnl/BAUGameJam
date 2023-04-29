using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private WeaponData _weaponData;
    private WaitForSeconds _lifeTime = new(5);
    private Coroutine disposeRoutine;

    public void Initialize(WeaponData weaponData, Vector2 direction)
    {
        _weaponData = weaponData;
        disposeRoutine = StartCoroutine(DisposeAfterLifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IEnemy enemy))
        {
            if (disposeRoutine != null)
                StopCoroutine(disposeRoutine);

            enemy.Hit(_weaponData.Damage);
            Dispose();
        }
    }

    private IEnumerator DisposeAfterLifeTime()
    {
        yield return _lifeTime;

        Dispose();
    }

    private void Dispose()
    {
        Destroy(gameObject);
    }
}
