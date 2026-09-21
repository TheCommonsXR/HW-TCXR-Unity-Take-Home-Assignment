using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Core;
using Platformer.Gameplay;

namespace Platformer.Mechanics
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 12f;
        public float lifetime = 3f;

        private Vector2 direction;
        private int damage;
        private Rigidbody2D body;
        
        private bool hasHit = false;
        
        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector2 fireDirection, int damageAmount)
        {
            direction = fireDirection;
            damage = damageAmount;
            body.velocity = direction * speed;
            Destroy(gameObject, lifetime);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (hasHit)
            {
                return;
            }

            var enemy = other.GetComponentInParent<EnemyController>();

            if (enemy == null)
            {
                return;
            }

            var enemyHealth = enemy.GetComponent<Health>();

            if (enemyHealth == null || !enemyHealth.IsAlive)
            {
                return;
            }

            hasHit = true;
            enemyHealth.TakeDamage(damage);

            if (!enemyHealth.IsAlive)
            {
                Simulation.Schedule<EnemyDeath>().enemy = enemy;
            }

            Destroy(gameObject);
        }
    }
}