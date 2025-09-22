using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20.0f;
    public Vector3 direction = Vector3.right;

    public int damage = 20;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore Collisions with Player
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            return;
        }

        // Hurt Enemy
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        // Destroy Bullet
        Destroy(gameObject);
    }

    // Destroy when outside of Screen
    private void OnBecameInvisible()
    {
        // Destroy Bullet
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Move the Bullet in a Specific Direction
        transform.position += direction * speed * Time.deltaTime;
    }
}
