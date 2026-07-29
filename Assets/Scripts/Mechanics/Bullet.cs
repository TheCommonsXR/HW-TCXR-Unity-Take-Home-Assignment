using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    private int damage;
    public int Damage => damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Set direction, speed, damage, and color
    public void Setup(bool direction, float speed, int damage, Color color)
    {
        rb.velocity = (direction ? Vector2.left : Vector2.right) * speed;
        this.damage = damage;
        sr.color = color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet on collision with walls or cactus
        if (collision.CompareTag("Walls") || collision.CompareTag("Cactus"))
        {
            Destroy(gameObject);
        }
    }
}
