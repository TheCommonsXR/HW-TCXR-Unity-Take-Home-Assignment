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
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Sets the entity's HP to the new value
        /// </summary>
        /// <param name="newValue">The new value to set the entity's current HP to.</param>
        public void SetHP(int newValue) {
            currentHP = newValue;
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            TakeDamage(1);
        }

        /// <summary>
        /// Reduces the HP of the entity by a given amount. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// <param name="damage">Specifies how much damage the entity will take.</param>
        /// </summary>
        public void TakeDamage(int damage) {
            if (!IsAlive) return;
            currentHP = Math.Clamp(currentHP - damage, 0, maxHP);
            if (currentHP == 0) {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
