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
                var playerHealth = player.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    if(player.health.isInvincible == false) //only apply damage if player is not invincible
                    {
                        enemyHealth.Enemy_Decrement(); //adjusted in health.cs to not have invincibility frames for enemies
                        playerHealth.Decrement(); //decrease player health when colliding on enemy
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
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
                player.health.DisplayHealth(); //displays current health and damage taken in console for feedback
            }
            else
            {
                Schedule<PlayerDeath>();
            }
        }
    }
}