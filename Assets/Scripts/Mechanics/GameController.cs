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
        public PlayerController player;
        public Gamemode gamemode;

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void OnEnable()
        {
            Instance = this;
            if (gamemode) // If a gamemode is input, use the values and apply them to the player
            {
                player.transform.position = gamemode.playerSpawn;
                player.health.maxHP = gamemode.playerHealth;
                player.health.Awake();
                player.bulletDamage = gamemode.gunDamage;
                player.maxSpeed = gamemode.playerSpeed;
                player.jumpTakeOffSpeed = gamemode.playerJumpPower;
            }
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