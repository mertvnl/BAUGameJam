using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    WeaponData WeaponData { get; }
    Transform FirePoint { get; }
    void Fire();
}
