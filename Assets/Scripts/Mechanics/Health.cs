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
        public bool isPlayer = false;

        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        [HideInInspector] public bool isImmune = false;

        [SerializeField] private float _immunityDuration = 1f;
        private int currentHP;
        
        private void Awake() => ResetHP();

        public void SetNewMaxHP(int newHP)
        {
            maxHP = newHP;
            ResetHP();
        }

        public void ResetHP() => currentHP = maxHP;

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
        public void Decrement(int damage = 1)
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

            StartCoroutine(ImmunityCoroutine());

            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        private IEnumerator ImmunityCoroutine()
        {
            isImmune = true;
            yield return new WaitForSeconds(_immunityDuration);
            isImmune = false;
        }
        /// <summary>
        /// Decrement the HP of the entity until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

    }
}
