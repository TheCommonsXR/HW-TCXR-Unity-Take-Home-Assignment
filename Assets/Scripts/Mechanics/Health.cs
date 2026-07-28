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
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(int dmg )//question1
        {
            currentHP = Mathf.Clamp(currentHP - dmg, 0, maxHP); //question1
            Debug.Log($"{currentHP}/{maxHP}"); //question1
            if (currentHP == 0 && GetComponent<PlayerController>() != null) //question 3 had to add this so player doesn't die
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

        public void SetHealth(int amount) // question 4
        {
            maxHP = amount;
            currentHP = amount;
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
