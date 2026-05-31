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
                if (player.Inviciblestate)
                    
                    return;


                Debug.Log("Enemy hit player");
                Health HP = player.GetComponent<Health>(); // (Q1) created a HP variable to compare to the health component

                if (HP != null)    
                {
                    Debug.Log("Player Health found");
                    HP.Decrement(enemy.injure);   // (Q1) if health component (HP) exist then calls decrement 

                    player.Inviciblestate = true;  // (Q2) When the player has been hit by the enemy the invinciblity state activates
                    player.Inviciblewindow = 1f;
                    Debug.Log("1 second invicibility state active ");
                }

                if (!HP.IsAlive)
                {
                    Debug.Log("Player Health is zero");
                    Schedule<PlayerDeath>();

                }
                

            }
        }
    }
}