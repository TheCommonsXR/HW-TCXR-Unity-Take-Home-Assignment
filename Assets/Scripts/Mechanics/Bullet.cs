using UnityEngine;
using Platformer.Mechanics;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage;

    private Vector2 direction;

    public void Initialize(Vector2 dir, int bulletDamage)
    {
        direction = dir.normalized;
        damage = bulletDamage;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.GetComponent<Health>();

        if (enemyHealth != null)
        {
            Debug.Log("Enemy is taking damage");
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}