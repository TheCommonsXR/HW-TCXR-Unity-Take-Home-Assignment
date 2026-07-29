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
                // Player does NOT have immunity
                if (!player.health.HasImmunity)
                {
                    // Show damage number based on damage dealt
                    player.health.SpawnDamageNumber(enemy.damage, player.health.damageNumberColor);

                    // Deal damage to player so long as they're alive
                    for (int i = 0; i < enemy.damage; i++)
                    {
                        if (player.health.IsAlive)
                            player.health.Decrement();
                        else
                            break;
                    }

                    player.health.GiveImmmunity();
                }
                // Player HAS immunity. Deal no damage
                else
                {
                    // Show that no damage is dealt
                    player.health.SpawnDamageNumber(0, player.health.immunityDamageNumberColor);
                }

                // Give the player knockback based on position of enemy
                player.ApplyKnockback(player.transform.position.x > enemy.transform.position.x);
            }
        }
    }
}