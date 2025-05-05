using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(SpriteRenderer))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f; // Speed of the bullet
        [SerializeField]
        private float lifetime = 3f; // Lifetime of the bullet in seconds
        [SerializeField]
        private int damage = 1; // Damage dealt by the bullet
        Rigidbody2D rigidBody;
        CircleCollider2D circleCollider;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            circleCollider = GetComponent<CircleCollider2D>();
            // Set the bullet's velocity
            //Vector2 direction = new Vector2(1, 0); // Change this to the desired direction
            //rigidBody.velocity = direction.normalized * speed;
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
            damage = newDamage;
        }

        public int GetDamage()
        {
            return damage;
        }

    }
}
