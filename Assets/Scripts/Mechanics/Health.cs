using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using System.Collections;

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
        public int maxHP = 3;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public int currentHP;

        private bool isInvulnerable = false;
        [SerializeField] private float invulnerabilityTime = 1f;

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
        ///

        public void Decrement()
        {
            TakeDamage(1);
        }
        public void TakeDamage(int damage)
        {
            if (!IsAlive || isInvulnerable)
                return;

            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

            StartCoroutine(Invulnerability());

            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }



        private IEnumerator Invulnerability()
        {   
            isInvulnerable = true;

            yield return new WaitForSeconds(invulnerabilityTime);

            isInvulnerable = false;
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            TakeDamage(currentHP);
        }

        void Awake()
        {
            currentHP = maxHP;
        }

        public void ResetHealth()
        {
            currentHP = maxHP;
        }

    }
}
