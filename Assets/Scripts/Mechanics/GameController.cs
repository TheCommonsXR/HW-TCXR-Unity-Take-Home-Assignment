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

        [Header("Game Mode Selection")]
        public GameMode selectedGameMode; // assign in Inspector

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
            if (selectedGameMode != null)
                ApplyGameMode(selectedGameMode);
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
        void ApplyGameMode(GameMode mode)
        {
            model.jumpModifier = mode.jumpModifier;
            model.jumpDeceleration = mode.jumpDeceleration;

            if (mode.spawnPoint != null)
                model.spawnPoint.position = mode.spawnPoint;

            Debug.Log("after spawnpoint");


            if (model.player != null)
            {
                Debug.Log("player not null");
                if (model.player.health != null)
                {
                    model.player.health.maxHP = mode.startingHealth;
                    model.player.health.currentHP = mode.startingHealth;
                }

                model.player.bulletDamage = mode.gunDamage;

                if (model.spawnPoint != null)
                {
                    Debug.Log("spawn not null");
                    model.player.transform.position = model.spawnPoint.position;
                }
            }
        }
    }
}