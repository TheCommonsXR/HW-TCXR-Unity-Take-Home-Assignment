using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using TMPro;
using Platformer.Model;
using Platformer.Core;

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

        public GameObject damageNumberPrefab;
        public Color damageNumberColor = Color.red;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);

            model.healthText.text = currentHP.ToString();
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

            model.healthText.text = currentHP.ToString();
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        /// <summary>
        /// Reset currentHP to maxHP
        /// </summary>
        public void ResetHealth()
        {
            currentHP = maxHP;
            model.healthText.text = currentHP.ToString();
        }

        /// <summary>
        /// Instantiate damage number
        /// </summary>
        public void SpawnDamageNumber(int damage)
        {
            // Spawn the damageNumber slightly above the player
            GameObject damageNumber = Instantiate(damageNumberPrefab, transform.position + Vector3.up * 0.25f, Quaternion.identity);
            // Give it damage value and color
            damageNumber.GetComponent<DamageNumber>().Setup(damage, damageNumberColor);
        }

        void Awake()
        {
            currentHP = maxHP;
            model.healthText.text = currentHP.ToString();
        }
    }
}
