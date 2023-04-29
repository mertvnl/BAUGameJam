using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }
    Transform T { get; }
    void Hit(int damage);
}
