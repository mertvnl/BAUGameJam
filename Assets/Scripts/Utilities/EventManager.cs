using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public static UnityEvent OnLevelFailed = new();
    public static UnityEvent OnLevelRestarted = new();
}
