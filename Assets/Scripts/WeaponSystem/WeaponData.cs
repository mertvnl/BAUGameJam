using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Create Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponTypes WeaponType;
    public GameObject Prefab;
    public float FireRate;
    public int Damage;
    public Sprite Icon;
    public int Price;
}