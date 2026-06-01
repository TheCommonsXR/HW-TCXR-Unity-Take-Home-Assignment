using Platformer.Mechanics;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public GameMode selectedGameMode;
    public Health playerHealth;
    public GunController playerGun;

    void Start()
    {
        ApplyGameMode();
    }

    void ApplyGameMode()
    {
        if (selectedGameMode == null)
            return;

        if (playerHealth != null)
        {
            playerHealth.SetMaxHealth(
                selectedGameMode.playerMaxHealth
            );

            playerHealth.SetImmunityTime(
                selectedGameMode.playerImmunitySeconds
            );
        }

        if (playerGun != null)
        {
            playerGun.gameModeConfig = selectedGameMode;
            playerGun.bulletDamage =
                selectedGameMode.bulletDamage;
        }
    }
}