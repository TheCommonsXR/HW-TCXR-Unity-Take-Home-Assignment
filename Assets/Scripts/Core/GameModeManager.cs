using UnityEngine;
using Platformer.Mechanics;

public class GameModeManager : MonoBehaviour
{
    public GameObject GameConfigPrefab;
    public PlayerController player;

    void Awake()
    {
        ApplyConfig();
    }

    /// <summary>
    /// Applies game configuration settings to player.
    /// </summary>
    private void ApplyConfig()
    {
        GameConfig config = GameConfigPrefab.GetComponent<GameConfig>();
        if (player != null)
        {
            player.maxSpeed = config.playerMaxSpeed;
            player.jumpTakeOffSpeed = config.playerJumpTakeOffSpeed;
            player.yDamage = config.playerYDamage;
            player.transform.position = config.playerSpawnPoint;
            player.health.maxHP = config.playerMaxHP;
            player.health.ResetHealth();
        }

    }
}
