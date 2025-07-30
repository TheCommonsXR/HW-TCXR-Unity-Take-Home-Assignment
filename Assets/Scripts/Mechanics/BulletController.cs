using Platformer.Mechanics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 100;
    public float lifetime = 2f;

    private Vector2 direction;

    public void Initialize(Vector2 fireDirection)
    {
        direction = fireDirection.normalized;
        Destroy(gameObject, lifetime); // Auto-cleanup
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}
