using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Damageable : MonoBehaviour
    {
        public Health health;
        public bool IsInvulnerable { get; private set; } = false;
        public float invulnerabilityTimer = 1f;

        private AudioSource audioSource;
        private Animator animator;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        public void Damaged(int amount, AudioClip ouchClip)
        {
            if (IsInvulnerable || health == null) return;
            health.TakeDamage(amount);

            if (health.IsAlive)
            {
                if (ouchClip != null)
                {
                    audioSource.PlayOneShot(ouchClip);

                    animator.SetTrigger("hurt");
                    StartCoroutine(InvulnerabilityCooldown());
                }
            }

            else
            {
                animator?.SetTrigger("die");
            }
        }

        private IEnumerator InvulnerabilityCooldown()
        {
            IsInvulnerable = true;
            yield return new WaitForSeconds(invulnerabilityTimer);
            IsInvulnerable = false;
        }
    }
}

