using Platformer.Core;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Gameplay
{

    public class BulletEnemyCollision : Simulation.Event<BulletEnemyCollision>
    {
        public EnemyController enemy;
        public Bullet bullet;

        public override void Execute()
        {
            Debug.Log("BulletEnemyCollision Executing...");
            var enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.DecreaseHealth(bullet.GetDamage());
                if (!enemyHealth.IsAlive)
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }
            }
            else
            {
                Schedule<EnemyDeath>().enemy = enemy;
            }
            // Destroy the bullet after it hits the enemy (just turn it off here and let the bullet clean itself up)
            if (bullet != null)
            {
                bullet.gameObject.GetComponent<SpriteRenderer>().enabled = false; // Hide the bullet
                bullet.gameObject.GetComponent<CircleCollider2D>().enabled = false; // Disable the collider
                bullet.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop the bullet
            }
        }
    }
}