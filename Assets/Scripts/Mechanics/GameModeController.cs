using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Applies a GameMode configuration to the scene on startup. A designer swaps
    /// game modes simply by assigning a different GameMode asset to activeMode in
    /// the inspector, then pressing Play. No code changes are needed to add or run
    /// a new mode.
    /// </summary>
    public class GameModeController : MonoBehaviour
    {
        public GameMode activeMode;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void Start()
        {
            if (activeMode != null)
                Apply(activeMode);
        }

        public void Apply(GameMode mode)
        {
            var player = model.player;
            if (player == null) return;
            if (model.spawnPoint != null)
                model.spawnPoint.position = mode.startPosition;
            player.Teleport(mode.startPosition);
            player.maxSpeed = mode.playerMaxSpeed;
            player.bulletDamage = mode.bulletDamage;
            if (player.health != null)
            {
                player.health.maxHP = mode.playerMaxHealth;
                player.health.Reset();
            }
            model.jumpModifier = mode.jumpModifier;
        }
    }
}
