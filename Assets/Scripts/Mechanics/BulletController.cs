using Platformer.Mechanics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private int direction = 1;
    private int damage = 1;

    public void Fire(int facingDirection, int bulletDamage)
    {
        direction = facingDirection;
        damage = bulletDamage;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyController>();

        if (enemy == null)
            return;

        var health = enemy.GetComponent<Health>();

        if (health != null)
        {
            for (int i = 0; i < damage; i++)
            {
                health.Decrement();
            }
        }

        Destroy(gameObject);
    }
}