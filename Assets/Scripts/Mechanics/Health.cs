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
        float protectionTime = 0; // New variable to control temporary protection time on entity spawn and entity getting hit

        [SerializeField] RectTransform healthBar; // New health bar object to visualize current health
        [SerializeField] Renderer entityRenderer; // New entity renderer variable to visualize protection time

        /// <summary>
        /// Updates the health bar of the entity if it exists.
        /// </summary>
        void UpdateHealthBar()
        {
            if (healthBar) healthBar.offsetMax = new Vector2(Mathf.Lerp(-0.8f, 0, (float)currentHP / maxHP), 0); // Divide current health by max health and use the percentage to resize the health bar filling
        }

        // <summary>
        // Start protection timer for a given amount of seconds
        // </summary>
        public void Protect(float time)
        {
            protectionTime = time;
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
        public void Decrement(int amount, bool protect) // Now decreases entity health using a parameter instead of only by 1 AND has a parameter to dictate if calling this method should invoke Protect
        {
            if (protectionTime == 0) // Only allow the entity to receive damage if there is no protection time
            {
                currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
                UpdateHealthBar(); // Update health bar whenever health is decremented
                if (currentHP == 0)
                {
                    var ev = Schedule<HealthIsZero>();
                    ev.health = this;
                } else {
                    if (protect) Protect(1); // If the entity has not died AND protect is true, give them 1 second of protection
                }
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            Decrement(currentHP, false); // Changed functionality to decrement entity's current health all at once instead of decrementing one HP at a time
        }

        public void Awake() // Changed to public to allow PlayerSpawn to reset player's health correctly upon respawning
        {
            currentHP = maxHP;
            UpdateHealthBar(); // Update health bar on spawn/respawn
            if (GetComponent<PlayerController>()) Protect(1.5f); // If the entity is a player, give them 1.5 seconds of spawn protection
        }

        void Update() // Added Update to run protection timer and protection visualizer
        {
            protectionTime = Mathf.Max(protectionTime - Time.deltaTime, 0);
            if (entityRenderer) entityRenderer.enabled = protectionTime % 0.1f < 0.05f; // Visualize protection time by flickering entity renderer
            // Turning the renderer off and on is a pretty common way to depict protection in games because the math is super simple
        }
    }
}
