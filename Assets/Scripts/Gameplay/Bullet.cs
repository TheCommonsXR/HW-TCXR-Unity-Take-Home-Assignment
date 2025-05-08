using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Diagnostics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Takes damage away from enemy when colliding with projectile.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public float speed = 25f;
        public Rigidbody2D rb;
        public int damage = 1;

        // Start is called before the first frame update
        void Start()
        {
            rb.velocity = transform.right * speed;
        }

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            EnemyController enemy = hitInfo.GetComponent<EnemyController>();
            if (enemy != null)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }

                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Supposed to destroy object after leaves camera view.
        /// </summary>
        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
    
}
