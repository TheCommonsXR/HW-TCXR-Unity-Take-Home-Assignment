using System;
using System.Collections;
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
        public int maxHP;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public bool isImmune = false;

        public int currentHP;

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
        public void Decrement(int amount)
        {
            currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
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
            while (currentHP > 0) Decrement(1);
        }

        /// <summary>
        /// Gives 'amount' second immunity after taking damage.
        /// </summary>
        public void Immunity(int amount)
        {
            StartCoroutine(ImmunityCoroutine(amount));
        }

        IEnumerator ImmunityCoroutine(int amount)
        {
            isImmune = true;
            Debug.Log("player isImmune true:" + isImmune);
            yield return new WaitForSeconds(amount);
            isImmune = false;
            Debug.Log("player isImmune false:" + isImmune);
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
