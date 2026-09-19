using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player health reaches 0. This usually would result in a 
    /// PlayerDeath event.
    /// </summary>
    /// <typeparam name="HealthIsZero"></typeparam>
    public class HealthIsZero : Simulation.Event<HealthIsZero>
    {
        public Health health;

        public override void Execute()
        {
            if (health.GetComponent<PlayerController>() != null) // runs whenever anyones health reaches 0. only starts PlayerDeath if player health reaches 0, without this hitting an enemy will also reduce player health
                Schedule<PlayerDeath>();
        }
    }
}