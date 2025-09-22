using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
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
        public int maxHP = 100;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        /// <summary>
        /// UI Label for Health
        /// </summary>
        public Text healthLabel;

        /// <summary>
        /// UI for Damage Dealt
        /// </summary>
        public DisplayDamageUI displayDamageUI;

        /// <summary>
        /// Immunity after Collision with Other Entity
        /// </summary>
        public float immunityTime = 1.0f; // Seconds
        float immunityTimer = 0.0f;
        bool activateImmunity = false;
        
        int currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Take X Amount of Damage. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void TakeDamage(int damage)
        {
            // Don't Apply Damage if currently immune
            if (activateImmunity)
                return;

            currentHP = (currentHP - damage > 0) ? currentHP - damage : 0; // Clip to 0

            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
            if (displayDamageUI != null)
            {
                displayDamageUI.DisplayDamage(damage);
            }
            UpdateUI();

            activateImmunity = true;
        }

        /// <summary>
        /// Reset the Health to the Maximum Amont
        /// </summary>
        public void ResetHP()
        {
            currentHP = maxHP;
            UpdateUI();
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void UpdateUI()
        {
            if (healthLabel != null)
            {
                healthLabel.text = currentHP.ToString();
            }
        }

        void Awake()
        {
            ResetHP();
        }

        private void Update()
        {
            // Apply Immunity until timer runs out
            if (activateImmunity)
            {
                immunityTimer += Time.deltaTime;
                if (immunityTimer > immunityTime)
                {
                    activateImmunity = false;
                    immunityTimer = 0.0f;
                }
            }
        }
    }
}
