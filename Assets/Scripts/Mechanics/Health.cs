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
        /// Indicates if the entity should be vulnerable. Should be set by external means.
        /// </summary>
        public bool IsVulnerable = true;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Sets the entity's HP to its maximum.
        /// </summary>
        public void SetToMax()
        {
            while (currentHP < maxHP) Increment();
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
                return true;
            }
            return false;
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

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
