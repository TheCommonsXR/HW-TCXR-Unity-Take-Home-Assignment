using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIDelegates
{
    public static event Action<int, int> OnHealthChanged;

    public static void InvokeOnHealthChanged(int newHealth, int newMaxHealth) {
        OnHealthChanged?.Invoke(newHealth, newMaxHealth);
    }
}
