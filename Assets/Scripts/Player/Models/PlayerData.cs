using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Create Player Data")]
public class PlayerData : ScriptableObject
{
    public int DefaultHealth = 100;
    public float DefaultSpeed = 10f;
}
