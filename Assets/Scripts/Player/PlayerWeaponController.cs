using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public IWeapon CurrentWeapon { get; private set; }

    private Player _player;
    public Player Player => _player ??= GetComponent<Player>();

    [SerializeField] private WeaponData defaultWeaponData;
    [SerializeField] private Transform weaponSpawnPoint;

    private IEnemy _closestEnemy;

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

        if (weaponSpawnPoint.localEulerAngles.z > 90 && weaponSpawnPoint.localEulerAngles.z < 270)
            weaponSpawnPoint.localScale = new Vector3(weaponSpawnPoint.localScale.x, -1f, weaponSpawnPoint.localScale.z);
        else
            weaponSpawnPoint.localScale = new Vector3(weaponSpawnPoint.localScale.x, 1f, weaponSpawnPoint.localScale.z);
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
                yield return new WaitForSeconds(GetFireRate());
            }

            yield return null;
        }
    }

    private float GetFireRate() 
    {
        float bonusFireRate = CurrentWeapon.WeaponData.FireRate * UpgradeManager.Instance.GetUpgradeByType(UpgradeType.AttackSpeed).GetCurrentValue() / 100f;
        float fireRate = CurrentWeapon.WeaponData.FireRate + bonusFireRate;
        return fireRate;
    }
}
