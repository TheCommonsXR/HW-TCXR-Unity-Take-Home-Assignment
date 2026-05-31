using Platformer.Core;
using Platformer.Model;
using UnityEngine;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Gamemodes gamechange;  // (Q4) Scriptable Object that stores the settings of the players health and starting position


        void Start()
        {
            if (gamechange == null)  // when no scriptable object game mode has been assigned return to original gameplay
            {
                return;

            }
                model.player.health.maxHP = gamechange.playerhp;    // sets players health
                model.player.maxSpeed = gamechange.playerSpeed;     // sets players speed 
                model.player.health.Increment(gamechange.playerhp);  // gives player the amount of health 
                model.player.Teleport(gamechange.playerPosition);    // Teleports player to the starting position
                if (model.spawnPoint != null)
                {
                   model.spawnPoint.transform.position = gamechange.playerPosition;  // Updates the spawn point to what the player puts 
                }
            
            // sets the setting for all the enemies 
            EnemyController[] enemies = Object.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

            foreach (EnemyController enemy in enemies)
            {
                Health enemyHealth = enemy.GetComponent<Health>(); // attaches health component to enemies 

                if (enemyHealth != null) // checks if enemy has health component 
                {
                    enemyHealth.maxHP = gamechange.enemyHp;    // sets max enemy HP
                    enemyHealth.Increment(gamechange.enemyHp); // gives the enemy the amount of hp
                
                }
            }

        }
        
        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
    }
}