using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int speed;
    public Vector2 direction;
    public EnemyController enemy;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
        Destroy(gameObject, 5f); // Destroy the bullet after 5 seconds to prevent clutter
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            var health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.Decrement();
            }
            //Destroy(gameObject);
        }
    }

   
}
