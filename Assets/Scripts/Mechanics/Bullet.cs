using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// The movement vector of the bullet in units per second
    /// </summary>
    public Vector3 movementVector;

    public float speed = 5;

    public float lifetime = 3;

    float timer;

    float damage = 1;

    public void Initialize(Vector2 move, float _damage)
    {
        Initialize(move);
        damage = _damage;
    }

    public void Initialize(Vector2 move)
    {
        movementVector = move.normalized * speed;
        timer = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementVector * Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyController = collision.gameObject.GetComponent<EnemyController>();

        if (enemyController)
        {
            var enemyHealth = enemyController.GetComponent<Health>();
            if (enemyHealth)
            {
                for (int i=0; i< (int)damage; i++)
                {
                    enemyHealth.Decrement();
                }
           
            }
            else
            {
                Schedule<EnemyDeath>().enemy = enemyController;
            }
            Destroy(gameObject);
        }

       
    }
}
