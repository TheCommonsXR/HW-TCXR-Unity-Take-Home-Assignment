using System;
using Platformer.Gameplay;
using UnityEngine;
using System.Collections;
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

        /// <summary>
        /// How long player stays invincible after taking damage.
        /// </summary>
        public float InvincibilityDuration = 1f;

        /// <summary>
        /// Used to manage player invincibility state.
        /// </summary>
        bool CanTakeDamage = true;

        int currentHP;
        

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            //currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            currentHP = maxHP;
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(int dmg)
        {
            StartCoroutine(InvinicibleOnCollision(dmg));
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement(currentHP);
        }

        void Awake()
        {
            currentHP = maxHP;
        }
        public IEnumerator InvinicibleOnCollision(int dmg)
        {
            if (!CanTakeDamage) yield break;
            CanTakeDamage = false;

            Debug.Log("Damage Received: " + dmg);
            Debug.Log("Health Before Hit: " + currentHP);
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
            currentHP -= dmg;
            if (currentHP <= 0)
            {
                Debug.Log("Health reached 0, firing HealthIsZero event.");
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
            Debug.Log("Health After Hit: " + currentHP);

            
            yield return new WaitForSeconds(InvincibilityDuration);
            CanTakeDamage = true;
        }
    }
}
