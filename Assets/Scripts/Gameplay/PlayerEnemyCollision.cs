using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            else
            {
                // processing damage
                if(!player.health.isInvincible)
                {
                    player.health.Decrement(enemy.damage);
                    player.SyncHealthBar();

                    if (!player.health.IsAlive)
                    {
                        Schedule<PlayerDeath>();
                    }
                    else
                    {
                        player.TakeDamageFeedback();
                        var knockbackDirection = player.transform.position.x - enemy.transform.position.x;
                        if (Mathf.Approximately(knockbackDirection, 0f))
                            knockbackDirection = 1f;

                        player.ApplyKnockback(knockbackDirection);
                    }
                }
                
                    
            }
        }
    }
}