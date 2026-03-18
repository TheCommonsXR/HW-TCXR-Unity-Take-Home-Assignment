using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Tooltip("Player only. Leave empty for enemies.")]
        public HealthBar optionalHealthBar;
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 10;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;
        public void SetCurrentHP(int hp)
        {
            currentHP = Mathf.Clamp(hp, 0, maxHP);
            UpdateHealthBar();
        }

        /// The last time the entity took damage, used for implementing temporary invincibility.
        float lastDamageTime = -1000f;

        [Tooltip("Player Only. The duration (in seconds) of temporary invincibility after taking damage. Leave it at 0 for enemies.")]
        public float immunityDuration = 0.0f;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment(int amount = 1)
        {
            currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
            UpdateHealthBar();
        }

        /// <summary>
        /// Check if the entity can take damage (immunity period expired).
        /// </summary>
        public bool CanTakeDamage()
        {
            return Time.time - lastDamageTime > immunityDuration;
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(int amount = 1)
        {
            currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
            lastDamageTime = Time.time;
            UpdateHealthBar();
            if (currentHP == 0 && optionalHealthBar != null)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Updates the health bar to reflect the current HP of the entity.
        /// </summary>
        public void UpdateHealthBar()
        {
            if (optionalHealthBar != null)
            {
                optionalHealthBar.UpdateHealthBar(currentHP, maxHP);
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) 
                Decrement();
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
