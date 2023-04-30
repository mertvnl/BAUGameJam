using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player _player;
    public Player Player => _player ??= GetComponent<Player>();

    [SerializeField] private WeaponData defaultWeaponData;
    [SerializeField] private Transform weaponSpawnPoint;

    private IEnemy _closestEnemy;

    public IWeapon CurrentWeapon { get; private set; }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        CurrentWeapon = Instantiate(defaultWeaponData.Prefab, weaponSpawnPoint).GetComponent<IWeapon>();
        HandleFireRate();
    }

    private void Update()
    {
        HandleWeaponRotation();
    }

    private void HandleWeaponRotation()
    {
        _closestEnemy = EnemyManager.Instance.GetClosestEnemy(Player.transform.position, UpgradeManager.Instance.GetUpgradeByType(UpgradeType.AttackRange).GetCurrentValue());

        if (_closestEnemy == null)
            return;

        weaponSpawnPoint.right =  weaponSpawnPoint.position - _closestEnemy.T.position;
    }

    private void HandleFireRate()
    {
        StartCoroutine(HandleFireRateCo());
    }

    private IEnumerator HandleFireRateCo()
    {
        while (true)
        {
            if (!Player.IsAlive)
                yield break;

            if (_closestEnemy != null)
            {
                CurrentWeapon.Fire();
                yield return new WaitForSeconds(CurrentWeapon.WeaponData.FireRate);
            }

            yield return null;
        }
    }
}
