using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }
    public bool IsControlable { get; private set; }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        IsControlable = true;
    }

    public void Die()
    {

    }
}
