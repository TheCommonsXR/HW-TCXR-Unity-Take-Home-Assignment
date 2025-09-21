using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay 
{
    /// <summary>
    /// Fired when a Player takes damage.
    /// </summary>
    public class PlayerDamaged : Simulation.Event<PlayerDamaged>
    {
        private PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public int damageAmount = 0;
        public override void Execute() 
        {
            if (!model.player.health.IsAlive) return;
            if (!model.player.health.IsVulnerable()) return;
            model.player.DoHurtEffect();
            model.player.health.TakeDamage(damageAmount);
            model.player.health.ResetDamageCooldownTimer();
        }
    }
    
}


