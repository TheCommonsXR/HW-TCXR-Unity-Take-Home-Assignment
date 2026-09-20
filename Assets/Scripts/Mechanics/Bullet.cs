using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 12f;
        public float lifetime = 3f;

        private Vector2 direction;

        public void Initialize(Vector2 fireDirection)
        {
            direction = fireDirection;
            Destroy(gameObject, lifetime);
        }

        void Update()
        {
            transform.position +=
                (Vector3)(direction * speed * Time.deltaTime);
        }
    }
}