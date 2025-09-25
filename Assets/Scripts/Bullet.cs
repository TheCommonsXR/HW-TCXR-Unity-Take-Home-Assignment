using UnityEngine;
using Platformer.Mechanics;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10.0f;
    public float lifetime = 3.0f;

    float currentLifeTimer;
    Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 dir, int dmg)
    {
        damage = dmg;
        direction = dir.normalized;
        currentLifeTimer = lifetime;
        gameObject.SetActive(true);
    }

    void Update()
    {
        currentLifeTimer -= Time.deltaTime;

        if (currentLifeTimer <= 0f)
        {
            BulletPool.Instance.ReturnToPool(this);
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            // Only deal damage to enemies
            if (other.GetComponent<PlayerController>() != null) return;

            int numDmg = 0;

            while (numDmg < damage)
            {
                health.Decrement();
                numDmg++;
            }
            Debug.Log(other.gameObject.name);
            BulletPool.Instance.ReturnToPool(this);
        } 
    }
}
