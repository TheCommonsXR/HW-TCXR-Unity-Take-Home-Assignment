using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;


public class Bullet : MonoBehaviour
{
    /*
        New class to create and move bullets once shot from the player 
        as well as inflict damage onto the enemy on contact 
    */

    public float speed = 10f;
    private Vector2 direction;
    private PlayerController shooter;

    public void Initialize(PlayerController player, Vector2 dir)
    {
        shooter = player;
        direction = dir.normalized;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            var enemyHealth = enemy.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.EnemyTakeDamage(shooter.bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
