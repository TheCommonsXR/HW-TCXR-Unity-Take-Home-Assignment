using UnityEngine;
using Platformer.Mechanics;

public class GameManager : MonoBehaviour
{
    public GameMode currentMode;
    public PlayerController player;

    void Start()
    {
        player.health.maxHP = currentMode.playerMaxHealth;
        player.health.ResetHealth();

        player.transform.position = currentMode.playerSpawnPosition;
    }
}