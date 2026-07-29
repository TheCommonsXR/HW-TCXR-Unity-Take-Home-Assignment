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
    /// Represents the current vital statistics of some game entity.
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
        public Color immunityDamageNumberColor = Color.white;
        public Color healNumberColor = Color.green;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public SpriteRenderer sr;
        [Range(0f, 1f)] public float immunityAlpha;

        public float immunityTime = 1f;
        bool hasImmunity;

        public bool isEnemy;

        /// <summary>
        /// Indicates if the player has immunity and should not recieve damage
        /// </summary>
        public bool HasImmunity => hasImmunity;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP++;

            if (!isEnemy) model.healthText.text = currentHP.ToString();
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0 && !isEnemy)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }

            if (!isEnemy) model.healthText.text = currentHP.ToString();
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
            if (!isEnemy) model.healthText.text = currentHP.ToString();
        }

        /// <summary>
        /// Instantiate damage number
        /// </summary>
        public void SpawnDamageNumber(int damage, Color color)
        {
            // Spawn the damageNumber slightly above the player
            GameObject damageNumber = Instantiate(damageNumberPrefab, transform.position + Vector3.up * 0.25f, Quaternion.identity);
            // Give it damage value and color
            damageNumber.GetComponent<DamageNumber>().Setup(damage, color);
        }

        /// <summary>
        /// After collision, give player immunity for x seconds
        /// </summary>
        public void GiveImmmunity()
        {
            hasImmunity = true;

            // Change alpha to show immunity duration
            Color spriteColor = sr.color;
            spriteColor.a = immunityAlpha;
            sr.color = spriteColor;

            Invoke("EndImmunity", immunityTime);
        }

        /// <summary>
        /// End immunity after x seconds
        /// </summary>
        void EndImmunity()
        {
            hasImmunity = false;

            // Return alpha to normal
            Color spriteColor = sr.color;
            spriteColor.a = 1f;
            sr.color = spriteColor;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (isEnemy) return;

            // Give player one health on collision with Health Crystal
            if (collision.CompareTag("Crystal"))
            {
                Increment();
                Destroy(collision.gameObject);
                SpawnDamageNumber(1, healNumberColor);
            }

            // Damage and knockback player if they run into Cactus
            if (collision.CompareTag("Cactus"))
            {
                Cactus cactus = collision.GetComponent<Cactus>();

                // Show damage number based on damage dealt
                SpawnDamageNumber(cactus.damage, damageNumberColor);

                // Deal damage to enemy so long as they're alive
                for (int i = 0; i < cactus.damage; i++)
                {
                    if (IsAlive)
                        Decrement();
                    else
                        break;
                }

                // Give immunity to player
                GiveImmmunity();

                // Give the player knockback based on position of enemy
                model.player.ApplyKnockback(model.player.transform.position.x > collision.transform.position.x);
            }
        }

        void Start()
        {
            currentHP = maxHP;
            if (!isEnemy) model.healthText.text = currentHP.ToString();
        }
    }
}
