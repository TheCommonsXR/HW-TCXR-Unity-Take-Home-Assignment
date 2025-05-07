using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public SpriteRenderer sprite;
    private int damage = 1;

    public void Launch(bool movingRight, int d) // Called as soon as the bullet spawns
    {
        damage = d; // Bullet's damage is set by player who fired it

        rigidbody.velocity = new Vector2(movingRight ? 4 : -4, 0); // Move bullet depending on where the player is facing

        sprite.flipX = !movingRight; // Flip the sprite if moving the other way

        Invoke("Despawn", 3); // Give the bullet a lifetime of 3 seconds
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.layer == 0) Despawn(); // If bullet hits terrain, despawn

        EnemyController enemy = hit.GetComponent<EnemyController>(); // Attempt to get the collider's enemy script

        if (enemy) // Attempt to find a valid enemy controller script within the target
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth) 
            {
                // If the enemy uses a health script, damage it using the bullet's damage value
                enemyHealth.Decrement(damage, false);
            } else { 
                // Otherwise, call EnemyDeath on the target enemy
                Schedule<EnemyDeath>().enemy = enemy;
            }
            
            Despawn();
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
