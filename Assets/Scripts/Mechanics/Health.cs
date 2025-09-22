using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour {
        public bool isPlayer = false;
        
        // Handle damage debounce
        public float takeDamageCooldown = 1f;
        private float takeDamageCooldownTimer = 0f;
        
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
                Schedule<HealthIsZero>().health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entity until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            currentHP = maxHP;
        }

        private void Update()
        {
            // Internal timer for damage debounce
            takeDamageCooldownTimer = Mathf.Clamp(takeDamageCooldownTimer - Time.deltaTime, 0f, takeDamageCooldown);
        }

        /// <summary>
        /// Returns true if entity is currently vulnerable to damage and false otherwise.
        /// </summary>
        /// <returns>A bool determining if entity is vulnerable to damage.</returns>
        public bool IsVulnerable()
        {
            return takeDamageCooldownTimer <= 0f;
        }

        /// <summary>
        /// Resets the internal damage cooldown timer, effectively making the entity invulnerable for takeDamageCooldown seconds.
        /// </summary>
        public void ResetDamageCooldownTimer()
        {
            takeDamageCooldownTimer = takeDamageCooldown;
        }

        /// <summary>
        /// Sets the entity's damage immunity window's duration to a new given value.
        /// </summary>
        /// <param name="newDamageCooldown">The new immunity duration for the entity after taking damage.</param>
        public void SetDamageCooldown(float newDamageCooldown) {
            takeDamageCooldown = newDamageCooldown;
        }

        /// <summary>
        /// Get's the entity's current health.
        /// </summary>
        /// <returns>The entity's current health.</returns>
        public int GetCurrentHP() {
            return currentHP;
        }
    }
}
