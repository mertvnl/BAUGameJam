using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Create Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int MaxHealth;
    public int MovementSpeed;
    public int Experience;
    public int AttackRange;
}
