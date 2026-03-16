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

        public GameModeConfig selectedGameMode;

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        private void Start()
        {
            ApplySelectedGameMode();
        }

        [ContextMenu("Apply Selected Game Mode")]
        public void ApplySelectedGameMode()
        {
            if (selectedGameMode == null || model == null || model.player == null) return;

            var player = model.player;
            var health = player.health != null ? player.health : player.GetComponent<Health>();
            if (health != null)
            {
                health.maxHP = Mathf.Max(1, selectedGameMode.playerMaxHealth);
                health.currentHP = Mathf.Clamp(selectedGameMode.playerStartHealth, 0, health.maxHP);
                health.isInvincible = false;
            }

            if (model.spawnPoint != null)
            {
                model.spawnPoint.position = selectedGameMode.playerStartPosition;
            }

            player.Teleport(selectedGameMode.playerStartPosition);
            player.jumpState = PlayerController.JumpState.Grounded;
            player.SyncHealthBar();
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
    }
}