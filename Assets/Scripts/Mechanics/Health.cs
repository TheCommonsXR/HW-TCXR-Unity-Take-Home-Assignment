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
        public int maxHP = 1;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;
        
        int currentHP;

        /// <summary>
        /// Indicates if the entity should be vulnerable. Should be set by external means.
        /// </summary>
        public bool IsVulnerable = true;

        public Slider uiHealthBar;


        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            SetUIHealth(currentHP);
        }

        /// <summary>
        /// Sets the entity's HP to its maximum.
        /// </summary>
        public void SetToMax()
        {
            while (currentHP < maxHP) Increment();
            SetUIHealth(currentHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public bool Decrement()
        {
            if (IsVulnerable)
            {
                currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
                if (currentHP == 0)
                {
                    var ev = Schedule<HealthIsZero>();
                    ev.health = this;
                }
                SetUIHealth(currentHP);
                return true;
            }
            return false;
        }

        public bool Decrement(int amount)
        {
            bool damaged = false;

            for (int i = 0; i < amount; i++)
            {
                if(currentHP > 0)
                {
                    if (Decrement() && !damaged)
                    {
                        damaged = true;
                    }
                }
            }

            return damaged;
        }

        public void MakeInvulnerable(float time)
        {
            IsVulnerable = false;
            var ev = Schedule<MakeVulnerable>(time);
            ev.health = this;
        }


        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
            IsVulnerable = false;
        }

        public void SetUIHealth(int health)
        {
            if (uiHealthBar)
            {
                uiHealthBar.maxValue = maxHP;
                uiHealthBar.value = health;
            }
        }

        void Awake()
        {
            currentHP = maxHP;

            if (uiHealthBar)
            {
                uiHealthBar.maxValue = maxHP;
            }
        }
    }
}
