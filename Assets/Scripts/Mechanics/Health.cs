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

        [SerializeField] RectTransform healthBar; // New health bar object to visualize current health

        /// <summary>
        /// Updates the health bar of the entity if it exists.
        /// </summary>
        void UpdateHealthBar()
        {
            if (healthBar) healthBar.offsetMax = new Vector2(Mathf.Lerp(-0.8f, 0, (float)currentHP / maxHP), 0); // Divide current health by max health and use the percentage to resize the health bar filling
        }

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UpdateHealthBar(); // Update health bar whenever health is incremented
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(int amount) // Now decreases entity health using a parameter instead of only by 1
        {
            currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
            UpdateHealthBar(); // Update health bar whenever health is decremented
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            Decrement(currentHP); // Changed functionality to decrement entity's current health all at once instead of decrementing one HP at a time
        }

        public void Awake() // Changed to public to allow PlayerSpawn to reset player's health correctly upon respawning
        {
            currentHP = maxHP;
            UpdateHealthBar(); // Update health bar on spawn/respawn
        }
    }
}
