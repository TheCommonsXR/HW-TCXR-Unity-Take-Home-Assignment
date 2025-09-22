using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDelegates
{
    public static event Action<int, int> OnPlayerHealthChanged;
    public static event Action<GameMode> OnGameModeChanged;
    public static event Action<bool> OnShowMainCanvasVisibility;

    public static void InvokeOnPlayerHealthChanged(int newHealth, int newMaxHealth) {
        OnPlayerHealthChanged?.Invoke(newHealth, newMaxHealth);
    }

    public static void InvokeOnGameModeChanged(GameMode newGameMode) {
        OnGameModeChanged?.Invoke(newGameMode);
    }
}
