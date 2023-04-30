using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [field: SerializeField] public WeaponData WeaponData { get; private set; }

    [field: SerializeField] public Transform FirePoint { get; private set; }

    protected float UpgradedFireRate => (WeaponData.FireRate + WeaponData.FireRate * UpgradeManager.Instance.GetUpgradeByType(UpgradeType.AttackSpeed).GetCurrentValue()) / 100;

    public virtual void Fire()
    {
        Bullet bullet = PoolingSystem.Instance.InstantiateFromPool("Bullet", FirePoint.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Initialize(WeaponData, FirePoint);
    }
}
