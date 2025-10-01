using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float lifetime = 3f;

        private Vector2 direction;
        private int damage;

        public void Init(Vector2 dir, int dmg)
        {
            direction = dir.normalized;
            damage = dmg;
            Destroy(gameObject, lifetime);
        }

        void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            var enemy = collider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                var ev = Schedule<BulletHitEnemy>();
                ev.enemy = enemy;
                ev.damage = damage;
                Destroy(gameObject);
            }
        }
    }
}

