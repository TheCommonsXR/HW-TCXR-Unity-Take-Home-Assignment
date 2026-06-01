using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A projectile fired by the player. Travels in a fixed direction and
    /// damages enemies on contact. The amount of damage (Y) is supplied by
    /// whoever fires the bullet so the value can stay specific to the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public int damage = 1;
        public float lifetime = 3f;

        Vector2 direction = Vector2.right;

        void Awake()
        {
            var body = GetComponent<Rigidbody2D>();
            body.gravityScale = 0;
            body.bodyType = RigidbodyType2D.Kinematic;
        }

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        public void Shoot(Vector2 direction)
        {
            this.direction = direction.normalized;
        }

        void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() != null)
                return;
            var enemy = other.GetComponent<EnemyController>();
            if (enemy != null) {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null) {
                    enemyHealth.Decrement(damage);
                    if (!enemyHealth.IsAlive)
                        Schedule<EnemyDeath>().enemy = enemy;
                } else {
                    Schedule<EnemyDeath>().enemy = enemy;
                }
                Destroy(gameObject);
            } else if (!other.isTrigger) {
                Destroy(gameObject);
            }
        }
    }
}
