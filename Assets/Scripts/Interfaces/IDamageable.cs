using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    Transform T { get; }
    void Hit(int damage);
}
