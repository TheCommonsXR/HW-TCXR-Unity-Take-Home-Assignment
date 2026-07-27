using System;
using Platformer.Gameplay;
using TMPro;
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
        public int maxHP;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;
        [SerializeField] TMP_Text hptext;
        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        /// 
        /// 
        /// 
        public void Awake()
        {
            currentHP = maxHP;
            UpdateHealthText();
        }
        public void Damage ( int damageAmount )
        {
            currentHP = Mathf.Clamp(currentHP - damageAmount, 0, maxHP);
            UpdateHealthText();
            Debug.Log($"[Health] Took {damageAmount} damage! Current HP: {currentHP}/{maxHP}");
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        public void RestoreHealth()
        {
            currentHP = maxHP;
            UpdateHealthText();
        }
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UpdateHealthText();
        }

      
        public void Decrement()
        {
            Damage(1);
        }

       
        public void Die()
        {
            Damage(currentHP);
        }
    private void UpdateHealthText()
        {
            hptext.text = $"HP: {currentHP}/{maxHP}";
        }
    }
}
