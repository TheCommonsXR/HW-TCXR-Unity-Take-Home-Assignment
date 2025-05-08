using System;
using System.Diagnostics;
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
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        /// <summary>
        /// Decrement the HP of the entitiy by set damage.
        /// </summary>
        public void TakeDamage (int damage)
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
            

            UnityEngine.Debug.Log("Damage Taken: " + damage);
            UnityEngine.Debug.Log("Current Health: " + currentHP);

            if (currentHP == 0)
            {
                UnityEngine.Debug.Log("Health Is Zero");
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }
        void Awake()
        {
            currentHP = maxHP;
        }

        /// <summary>
        /// Gets the current HP.
        /// </summary>

        public int GetCurrentHP()
        {
            return currentHP;
        }
    }
}
