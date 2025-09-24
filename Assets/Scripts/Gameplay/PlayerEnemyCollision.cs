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
                if (player.isInvulnerable) return;
                
                    // Instead instantly killing the player when colliding with the 
                    // we simply decrease their health by that enemy's damage value
                    player.health.TakeDamage(enemy.enemyDamage);
                if (player.health.IsAlive)
                {
                    player.audioSource.PlayOneShot(player.ouchAudio);
                    player.animator.SetTrigger("hurt");

                    // Upon taking damage the temporary invinciblity is set 
                    player.isInvulnerable = true;
                    // The cooldown timer immediately starts and make the player vulnerable again after one second
                    player.StartCoroutine(player.InvulnerabilityCooldown());
                        
                }
            }
        }
    }
}