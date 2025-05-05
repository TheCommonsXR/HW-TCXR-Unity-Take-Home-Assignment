using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(Damage))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 100)]
        private float speed = 10f; // Speed of the bullet
        [SerializeField]
        [Range(0, 10)]
        private float lifetime = 3f; // Lifetime of the bullet in seconds
        Damage damage;
        Rigidbody2D rigidBody;
        CircleCollider2D circleCollider;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            circleCollider = GetComponent<CircleCollider2D>();
            damage = GetComponent<Damage>();
            // Set the bullet's velocity
            StartCoroutine(TimeToDestruction());
        }

        public void SetDirection(Vector2 newDirection)
        {
            rigidBody.velocity = newDirection.normalized * speed;
        }

        IEnumerator TimeToDestruction()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        public void SetDamage(int newDamage)
        {
            damage.SetDamageAmount(newDamage);
        }

        public int GetDamage()
        {
            return damage.GetDamageAmount();
        }

    }
}
