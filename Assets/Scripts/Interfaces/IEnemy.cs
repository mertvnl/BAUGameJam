using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : IDamageable
{
    bool IsAlive { get; }
}
