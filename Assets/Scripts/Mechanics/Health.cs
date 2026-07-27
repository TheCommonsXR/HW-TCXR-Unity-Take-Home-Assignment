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
     
        public int maxHP;

    public float immunityduration;
    public float immunitytimer;
    public bool IsInvulnerable => immunitytimer > 0;   
        public bool IsAlive => currentHP > 0;

        int currentHP;
        [SerializeField] TMP_Text hptext;
   
        public void Awake()
        {
            currentHP = maxHP;
            UpdateHealthText();
        }

         public void Update()
        {
            if (immunitytimer > 0)
            {
                immunitytimer -= Time.deltaTime;
            }
        }  

        public void TriggerTimer()
        {
            immunitytimer = immunityduration;
        }
 
        public void Damage ( int damageAmount )
        {
            if (IsInvulnerable) return;
            Debug.Log("Collision");
            currentHP = Mathf.Clamp(currentHP - damageAmount, 0, maxHP);
            UpdateHealthText();
            Debug.Log($"[Health] Took {damageAmount} damage! Current HP: {currentHP}/{maxHP}");
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            } else
            {
                TriggerTimer();
            }
        }

        public void RestoreHealth()
        {
            currentHP = maxHP;
            immunitytimer = 0f;
            UpdateHealthText();
        }
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UpdateHealthText();
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
