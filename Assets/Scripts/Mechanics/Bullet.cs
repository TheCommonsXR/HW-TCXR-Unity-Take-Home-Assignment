using UnityEngine;
using Platformer.Mechanics;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represents a bullet that can damage entities with a Health component.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public int damage = 1;

        void OnTriggerEnter2D(Collider2D other)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }
}