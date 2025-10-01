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
    private int damage;

    public void Initialize(int bulletDamage, Vector2 dir)
    {
        damage = bulletDamage;
        direction = dir.normalized;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
