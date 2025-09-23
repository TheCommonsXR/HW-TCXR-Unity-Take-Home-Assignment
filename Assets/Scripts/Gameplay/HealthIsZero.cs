using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the entity health reaches 0. This usually would result in a 
    /// EnemyDeath or PlayerDeath event.
    /// </summary>
    /// <typeparam name="HealthIsZero"></typeparam>
    public class HealthIsZero : Simulation.Event<HealthIsZero>
    {
        public Health health;

        public override void Execute()
        {
            if (health.isPlayer)
            {
                Schedule<PlayerDeath>();
            }
            else
            {
                Schedule<EnemyDeath>().enemy = health.GetComponent<EnemyController>();
            }
        }
    }
}