using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class GameModeManager : MonoBehaviour
    {
    public GameModeConfig activeConfig;
    public PlayerController player;
    public EnemyController enemy;

    // Start is called before the first frame update
        void Start()
        {
            if (activeConfig != null && player != null)
            {
                // Sets the players maxHp to set value 
                player.health.maxHP = activeConfig.playerStartingHealth;
                player.health.ResetHealth();

                // Sets the player's inital starting point 
                player.transform.position = activeConfig.playerSpawnPoint;

                foreach (var enemy in enemies)
                {
                    // Same with the enemy hp
                    enemy.enemyMaxHealth = activeConfig.enemyStartingHealth;
                    enemyResetHealth();
                }
                

                
            }
        }


    }
}

