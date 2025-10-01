using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represents the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHP = 5;
        [SerializeField] int currentHP = 0;

        /// <summary>
        /// Public read/write for max HP (keeps internal consistency).
        /// </summary>
        public int MaxHP
        {
            get => maxHP;
            set
            {
                maxHP = Mathf.Max(1, value);
                if (currentHP > maxHP) currentHP = maxHP;
            }
        }

        /// <summary>
        /// Read-only current HP (external scripts should use methods below).
        /// </summary>
        public int CurrentHP => currentHP;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        void Awake()
        {
            // initialize currentHP to max if not set
            if (currentHP <= 0) currentHP = maxHP;
        }

        /// <summary>
        /// Set both max and current HP in a safe way.
        /// </summary>
        public void SetHealth(int newMaxHP, int newCurrentHP)
        {
            MaxHP = newMaxHP;
            currentHP = Mathf.Clamp(newCurrentHP, 0, maxHP);
        }

        /// <summary>
        /// Restore to full health.
        /// </summary>
        public void FullHeal()
        {
            currentHP = maxHP;
        }

        /// <summary>
        /// Increment the HP by one (clamped).
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity by a specified amount.
        /// Will trigger a HealthIsZero event when current HP reaches 0.
        /// </summary>
        public void Decrement(int damage = 1)
        {
            if (damage <= 0) return;
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
            Debug.Log($"Took {damage} damage. HP is now {currentHP}/{maxHP}");
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Instantly kill the entity (sets HP to zero).
        /// </summary>
        public void Die()
        {
            currentHP = 0;
            var ev = Schedule<HealthIsZero>();
            ev.health = this;
        }
    }
}
