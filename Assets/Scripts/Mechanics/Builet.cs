using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class Builet : MonoBehaviour
{
    public float speed = 10f; // bullet travel speed
    public float damage; // damage dealt to enemy
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    /// <summary>
    /// Set direction and damage of the bullet.
    /// </summary>
    public void SetDirDmg(Vector2 direction, float yDamage)
    {
        rb.velocity = direction * speed;
        damage = yDamage;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy(gameObject, 3f);
    }

    /// <summary>
    /// Checks for collision with enemy and applies damage to enemy if hit.
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
