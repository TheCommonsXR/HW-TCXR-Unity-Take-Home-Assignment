using UnityEngine;
using Platformer.Mechanics;

public class GameManager : MonoBehaviour
{
    public GameModeConfig currentGameMode;

    public PlayerController player;

    private void Start()
    {
        if (currentGameMode == null || player == null)
            return;

        ApplyGameMode();
    }

    private void ApplyGameMode()
    {
        // Position
        player.transform.position =
            currentGameMode.playerStartPosition;

        // Health
        player.health.maxHP =
            currentGameMode.playerMaxHealth;

        player.health.setMaxHp();

        // Speed
        player.maxSpeed =
            currentGameMode.playerSpeed;
    }
}