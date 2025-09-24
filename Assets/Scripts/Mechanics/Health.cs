using System.Collections;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using Random = System.Random;

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
        public int maxHP = 50; //changed from 1 to 50 to give player more health

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;
        public bool isInvincible = false; //flag to indicate if player is currently invincible

        public int currentHP;
        int Player_damage;

        IEnumerator InvincibilityFrames(float duration) //function to handle invincibility frames
        {
            isInvincible = true;
            yield return new WaitForSeconds(duration);
            isInvincible = false;
        }

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            //currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            currentHP = maxHP; //changed to always restore to max health when respawning cdew   
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            Random random = new Random();
            Player_damage = random.Next(1, 8); //decrease health by a random amount between 1 and 7 when hit by an enemy
            currentHP = currentHP - Player_damage;
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            float duration = 1.0f; // Duration of the invincibility frames
            StartCoroutine(InvincibilityFrames(duration));
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }
        public void Enemy_Decrement()
        {
            Random random = new Random();
            Player_damage = random.Next(1, 8); //decrease health by a random amount between 1 and 7 when hit by an enemy
            currentHP = currentHP - Player_damage;
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

        void Awake()
        {
            currentHP = maxHP;
        }
        public void DisplayHealth() //Displays current health and damage taken in console for feedback
        {
            Debug.Log("Damage Taken: " + Player_damage);
            Debug.Log("Current Health: " + currentHP);
        }
    }
}