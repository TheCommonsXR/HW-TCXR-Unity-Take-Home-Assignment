using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Bullet collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class BulletEnemyCollision : Simulation.Event<BulletEnemyCollision>
    {
        public EnemyController enemy;
        public Bullet bullet;

        public override void Execute()
        {
            
            var enemyHealth = enemy.health;
            if (enemyHealth != null)
            {
                enemyHealth.Decrement(bullet.damage);
                if (!enemyHealth.IsAlive)
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }
                else
                {
                    Schedule<EnemyDamaged>().enemy = enemy;
                }
            }
            else
            {
                Schedule<EnemyDeath>().enemy = enemy;
            }

            bullet.gameObject.SetActive(false);
            
        }
    }
}