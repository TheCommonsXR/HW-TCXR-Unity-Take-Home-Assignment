using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class Bullets : MonoBehaviour
    {
        public float speed = 100f; //speed of a bullet
        public Rigidbody2D rb;
        public int damage = 5; // Damage dealt to the enemy
        public GameObject impactEffect;
        int rounds = 10; //number of bullets before needing to reload

        // Start is called before the first frame update
        void Start()
        {
            rb.velocity = transform.right * speed;
        }
    
        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            EnemyController enemy = hitInfo.GetComponent<EnemyController>();
            if (enemy != null)
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.currentHP -= damage; //Reducesses enemy health by bullet damage
                    if (enemyHealth.currentHP <= 0)
                    {
                        enemyHealth.Die();
                    }
                }
            }

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject); // Destroy the bullet sprite on impact
        }
        public void Fire()
            {
                if (rounds > 0)
                {
                    // Logic to fire a bullet
                    rounds--;
                }
                else
                {
                    Debug.Log("Out of ammo, Reloading");
                    rounds = 10; // Resets rounds to 10 after reloading
                }
            }
    }
}