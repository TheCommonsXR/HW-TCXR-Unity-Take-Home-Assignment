using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a bullet collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class BulletEnemyCollision : Simulation.Event<BulletEnemyCollision>
    {
        public EnemyController enemy;
        public GameObject bullet;

        // PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var enemyHealth = enemy.GetComponent<Health>();
            if (bullet != null && enemyHealth != null){
                enemyHealth.Decrement(1);
                if (!enemyHealth.IsAlive)
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }
            }
            Object.Destroy(bullet);
        }
    }
}