using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;
namespace Platformer.Gameplay
{


    public class Bullet : MonoBehaviour
    {
        public int bulletdamage = 1; // the amount of damge the bullet deals

        private void OnTriggerEnter2D(Collider2D other)

        {
            
            EnemyController enemy = other.GetComponentInParent<EnemyController>();

            if (enemy != null)
            {
               
               Health enemyHP = enemy.GetComponentInParent<Health>(); // Looks for the enemy Controller/parent when the bullet hits 


                // Checks to see if the enemy is found and has the health component 
                if (enemyHP != null)
                {
                   
                    enemyHP.Decrement(bulletdamage); // reduces enemy health by the amount the bullet damge does 



                    // Once enemy health is 0 it activates the enemy death function
                    if (!enemyHP.IsAlive)
                    {
                        
                        Schedule<EnemyDeath>().enemy = enemy;

                    }
                }
               
                    Destroy(gameObject); // Destroys bullet after hitting the enemy
            }

            
            

        }
        
         


    }
}
