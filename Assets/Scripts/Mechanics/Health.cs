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
        public int maxHP=5;
        float UIHealthBarMaxHP;
        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public Image UIHealthBar;


        int currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        // public void baseMaxHP(int _health)
        // {
        //     maxHP = _health;
        // }
        public void setMaxHp()
        {
            fillUIHealth();
            while (currentHP < maxHP) Increment();
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
        public void Decrement(int amount)
        {
            currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
            //Debug.Log(currentHP);
            //reduce health bar
            UIHealthBar.fillAmount=(float)currentHP/UIHealthBarMaxHP;
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
        public int getCurrentHP(){
            return currentHP;
        }
         public void fillUIHealth(){
             UIHealthBar.fillAmount=(float)maxHP; 
         }
        void Awake()
        {
            currentHP = maxHP;

            UIHealthBarMaxHP=(float)maxHP;
        }
    }
}
