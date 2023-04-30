using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public IWeapon CurrentWeapon { get; private set; }

    private Player _player;
    public Player Player => _player ??= GetComponent<Player>();

    [SerializeField] private WeaponData defaultWeaponData;
    [SerializeField] private Transform weaponHolder;
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
        _closestEnemy = EnemyManager.Instance.GetClosestEnemy(Player.transform.position, GetRange());

        if (_closestEnemy == null)
            return;

        weaponHolder.right =  weaponHolder.position - _closestEnemy.T.position;

        if (weaponHolder.localEulerAngles.z > 90 && weaponHolder.localEulerAngles.z < 270)
            weaponHolder.localScale = new Vector3(weaponHolder.localScale.x, -1f, weaponHolder.localScale.z);
        else
            weaponHolder.localScale = new Vector3(weaponHolder.localScale.x, 1f, weaponHolder.localScale.z);
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

    private float GetRange() 
    {
        float defaultRange = UpgradeManager.Instance.GetUpgradeByType(UpgradeType.AttackRange).DefaultUpgradeValue;
        float bonusRange = defaultRange + defaultRange * UpgradeManager.Instance.GetUpgradeByType(UpgradeType.AttackRange).GetCurrentValue() / 100f;
        return defaultRange + bonusRange;
    }

    private float GetFireRate() 
    {
        float bonusFireRate = CurrentWeapon.WeaponData.FireRate * UpgradeManager.Instance.GetUpgradeByType(UpgradeType.AttackSpeed).GetCurrentValue() / 100f;
        float fireRate = CurrentWeapon.WeaponData.FireRate + bonusFireRate;
        return 1f / fireRate;
    }
}
