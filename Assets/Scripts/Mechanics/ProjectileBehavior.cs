using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class ProjectileBehavior : MonoBehaviour
    {
        [Tooltip("Speed of projectile.")]
        public float ProjectileSpeed = 10f;

        [Tooltip("Time in seconds before the projectile is destroyed.")]
        public float LifeTime = 5f;

        int damage;
        Vector2 direction;
        GameObject Player;
        // Start is called before the first frame update
        void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            StartCoroutine(DestroyTimer());
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(direction.normalized * ProjectileSpeed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                var enemyHealth = enemy.gameObject.GetComponent<Health>();
                if (enemy && enemyHealth != null)
                {
                    enemyHealth.Decrement(damage);
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }

            }

        }

        public void SetDirection(Vector2 dir)
        {
            direction = dir;
        }

        public void SetDamage(int dmg)
        {
            damage = dmg;
        }

        public IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(LifeTime);
            Destroy(gameObject);
        }
    }
}

