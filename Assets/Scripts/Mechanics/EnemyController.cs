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

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        // Amount of damage the player take when collliding with enemy
        public int enemyDamage = 1;
        public int enemyMaxHealth = 5;

        int currentEnemyHealth;

        // Allows the enemy to take damage forom the bullents 
        // based on the bullet damage value t

        public void EnemyTakeDamage(int amount)
        {
            currentEnemyHealth = Mathf.Clamp(currentEnemyHealth - 1, 0, enemyMaxHealth);

            if (currentEnemyHealth == 0)
            {
                Destroy(gameObject);
            }
        }

        public void ResetHealth()
        {
            currentEnemyHealth = enemyMaxHealth;
        }

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentEnemyHealth = enemyMaxHealth;
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