using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public float speed = 20f;
    private Vector2 direction = Vector2.right;  // Default direction

    private void Start()
    {
        DestroyGameObject();
    }

    /// <summary>
    /// Set the direction the bullet should travel.
    /// </summary>
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject,5f);
    }
}
