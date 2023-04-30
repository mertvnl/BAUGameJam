using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody ??= GetComponent<Rigidbody2D>();

    private WeaponData _weaponData;
    private WaitForSeconds _lifeTime = new(5);
    private Coroutine disposeRoutine;

    private bool _isInitialized;

    private const float BULLET_VELOCITY = 950f;

    public void Initialize(WeaponData weaponData, Transform firePoint)
    {
        transform.right = firePoint.right;
        _weaponData = weaponData;
        disposeRoutine = StartCoroutine(DisposeAfterLifeTime());

        _isInitialized = true;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!_isInitialized)
            return;

        Rigidbody.velocity = BULLET_VELOCITY * Time.fixedDeltaTime * -transform.right;
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
