using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        public float lifetime = 2f;

        Rigidbody2D body;
        int damage;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        public void Initialize(float direction, float speed, int damageAmount)
        {
            damage = damageAmount;
            body.velocity = new Vector2(direction * speed, 0f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement(damage);
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
                return;
            }

            // Destroy on world collision, but ignore triggers and player.
            if (!other.isTrigger && other.GetComponent<PlayerController>() == null)
            {
                Destroy(gameObject);
            }
        }
    }
}



