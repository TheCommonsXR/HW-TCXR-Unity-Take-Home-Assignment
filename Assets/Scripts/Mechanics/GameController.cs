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

        public GameMode gameMode;

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }
    public void Start()
        {
            NewGameMode();
        }
        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
        public void NewGameMode()
        {
            Debug.Log("Game Mode: " + gameMode.gameModeName);
            model.player.transform.position = gameMode.playerStartingPosition;
            Debug.Log("Spawned Position: " + gameMode.playerStartingPosition);
            Health health = model.player.GetComponent<Health>();
            health.maxHP = gameMode.startingPlayerHealth;
            health.RestoreHealth();
            Debug.Log("Player Max HP : " + health.maxHP);
            playershooting shooting = model.player.GetComponent<playershooting>();
            shooting.bulletDamage = gameMode.startingbulletdamage;
            //shooting.UpdateHealthText();
            Debug.Log("Bullet Damage: " + shooting.bulletDamage);
            model.spawnPoint.position = gameMode.playerStartingPosition;
        }
    }
}