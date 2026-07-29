using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;
        public int damage = 1;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;
        Health health;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            health = GetComponent<Health>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // Hit by player bullet!
            if (collision.CompareTag("Bullet"))
            {
                // Damage enemy
                if (health != null)
                {
                    Bullet bullet = collision.gameObject.GetComponent<Bullet>();

                    // Show damage number based on damage dealt
                    health.SpawnDamageNumber(bullet.Damage, health.damageNumberColor);

                    // Deal damage to enemy so long as they're alive
                    for (int i = 0; i < bullet.Damage; i++)
                    {
                        if (health.IsAlive)
                            health.Decrement();
                        else
                            break;
                    }

                    if (!health.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = this;
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = this;
                }

                // Destroy Bullet
                Destroy(collision.gameObject);
            }
        }

        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

    }
}