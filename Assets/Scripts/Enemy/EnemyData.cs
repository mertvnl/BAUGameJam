using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Create Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int MaxHealth;
    public int Damage;
    public int MovementSpeed;
    public int Experience;
    public float AttackRange;
    public float AttackCooldown;
    public GameObject EnemyPrefab; 
}
