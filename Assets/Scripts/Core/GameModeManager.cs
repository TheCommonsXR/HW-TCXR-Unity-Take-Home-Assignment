using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class GameModeManager : MonoBehaviour
{
    public GameMode currentMode;
    public PlayerController player;

    void Start()
    {
        ApplyGameMode();
    }

    public void ApplyGameMode()
    {
        if (currentMode != null && player != null)
        {
            player.maxSpeed = currentMode.moveSpeed;
            player.health.SetHealth(currentMode.startingHealth, currentMode.startingHealth);
            player.transform.position = currentMode.spawnPosition;
            player.canShoot = currentMode.allowShooting;

            Debug.Log($"Game Mode '{currentMode.modeName}' applied!");
        }
        else
        {
            Debug.LogWarning("GameModeManager: Missing reference to currentMode or player.");
        }
    }

}
