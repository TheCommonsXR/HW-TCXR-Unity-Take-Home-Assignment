using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class GameModeManager : MonoBehaviour
    {
    public GameModeConfig activeConfig;
    public PlayerController player;
    public EnemyController[] enemies;

        // Start is called before the first frame update
        void Start()
        {
            if (activeConfig == null) return;

            if (player != null)
            {
                // Sets the hp, and starting position of the player
                player.ApplyGameModeSettings(activeConfig);
            }
            
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    // Same with the enemy hp
                    enemy.ApplyGameModeSettingsEnemy(activeConfig);
                }
                
            }
        }


    }
}

